import { Box, Checkbox, Counter, Flexbox, FormField, Select, Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetTerminals } from "api/terminal.queries";
import { Direction } from "common/types/terminals/direction";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { Controller, useFieldArray, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import FormSection from "../FormSection";
import SelectItemDialog from "../SelectItemDialog";
import { BlockFormFields, onAddTerminals, resolveSelectedAndAvailableTerminals } from "./BlockForm.helpers";
import { prepareTerminals } from "./prepareTerminals";

/**
 * Component which contains all simple value fields of the terminal form.
 *
 * @constructor
 */

const FormTerminals = () => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const { control, register, setValue } = useFormContext<BlockFormFields>();

  const terminalFields = useFieldArray({ control, name: "terminals" });
  const terminalQuery = useGetTerminals();
  const terminals = prepareTerminals(terminalQuery.data) ?? [];
  const [available, selected] = resolveSelectedAndAvailableTerminals(terminalFields.fields, terminals);
  const terminalTypeRefs = useWatch({ control, name: "terminals" });

  const connectorDirectionOptions = getOptionsFromEnum<Direction>(Direction);

  const directionOptions = (terminalId: string | undefined) => {
    if (!terminalId) return connectorDirectionOptions;

    const terminal = terminals.find((x) => x.id === terminalId);

    if (terminal?.qualifier !== Direction.Bidirectional) {
      const option = connectorDirectionOptions.find((x) => x.value === terminal?.qualifier);
      return option ? [option] : [];
    }

    return connectorDirectionOptions.filter(
      (x) =>
        !terminalTypeRefs
          .filter((y) => y.terminal.id === terminalId)
          .map((y) => y.direction)
          .includes(x.value),
    );
  };

  return (
    <FormSection
      title={t("block.terminals.title")}
      action={
        <SelectItemDialog
          title={t("block.terminals.dialog.title")}
          description={t("block.terminals.dialog.description")}
          searchFieldText={t("block.terminals.dialog.search")}
          addItemsButtonText={t("block.terminals.dialog.add")}
          openDialogButtonText={t("block.terminals.open")}
          items={available}
          onAdd={(ids) => onAddTerminals(ids, terminals, terminalTypeRefs, terminalFields.append)}
        />
      }
    >
      <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.xl}>
        {terminalFields.fields.map((field, index) => {
          const terminal = selected.find((x) => x.id === field.terminal.id);
          return (
            terminal && (
              <Flexbox alignItems={"center"} key={field.id}>
                <Box flex={1}>
                  <Token
                    variant={"secondary"}
                    {...register(`terminals.${index}`)}
                    actionable
                    actionIcon={<XCircle />}
                    actionText={t("block.terminals.remove")}
                    onAction={() => terminalFields.remove(index)}
                    dangerousAction
                  >
                    {terminal.name}
                  </Token>
                </Box>
                <Box>
                  <Controller
                    control={control}
                    name={`terminals.${index}.minCount`}
                    render={({ field: { onChange, ...rest } }) => (
                      <Counter
                        {...rest}
                        min={0}
                        onChange={(value) => {
                          const currentMaxCount = field.maxCount;
                          if (currentMaxCount && currentMaxCount < value) {
                            setValue(`terminals.${index}.maxCount`, value);
                          }
                          onChange(value);
                        }}
                      />
                    )}
                  />
                </Box>
                <Box>
                  <Checkbox
                    onCheckedChange={(checked) => {
                      if (checked) {
                        if (field.minCount > 0) {
                          setValue(`terminals.${index}.maxCount`, field.minCount);
                        } else {
                          setValue(`terminals.${index}.maxCount`, 1);
                        }
                      } else {
                        setValue(`terminals.${index}.maxCount`, undefined);
                      }
                    }}
                  />
                </Box>
                <Box>
                  <Controller
                    control={control}
                    name={`terminals.${index}.maxCount`}
                    render={({ field: { value, ...rest } }) => (
                      <Counter {...rest} min={Math.max(field.minCount, 1)} value={value ?? 0} disabled={!value} />
                    )}
                  />
                </Box>

                <FormField>
                  <Controller
                    control={control}
                    name={`terminals.${index}.direction`}
                    render={({ field: { value, onChange, ref, ...rest } }) => (
                      <Select
                        {...rest}
                        selectRef={ref}
                        placeholder={t("common.templates.select", { object: t("block.terminal.name").toLowerCase() })}
                        options={directionOptions(field.terminal.id)}
                        getOptionLabel={(x) => x.label}
                        onChange={(x) => {
                          onChange(x?.value);
                        }}
                        value={connectorDirectionOptions.find((x) => x.value === value)}
                      />
                    )}
                  />
                </FormField>
              </Flexbox>
            )
          );
        })}
      </Flexbox>
    </FormSection>
  );
};

export default FormTerminals;
