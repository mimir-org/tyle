import { Flexbox } from "@mimirorg/component-library";
import { useGetTerminals } from "api/terminal.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { Controller, useFieldArray, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Direction } from "types/terminals/direction";
import { getOptionsFromEnum } from "utils";
import { BlockFormFields, onAddTerminals, resolveSelectedAndAvailableTerminals } from "./BlockForm.helpers";
import TerminalRow from "./TerminalRow";
import { prepareTerminals } from "./prepareTerminals";

/**
 * Component which contains all simple value fields of the terminal form.
 *
 * @constructor
 */

const FormTerminals = () => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const { control } = useFormContext<BlockFormFields>();

  const terminalFields = useFieldArray({ control, name: "terminals" });
  const terminalQuery = useGetTerminals();
  const terminals = prepareTerminals(terminalQuery.data) ?? [];
  const [available, _] = resolveSelectedAndAvailableTerminals(terminalFields.fields, terminals);
  const terminalTypeRefs = useWatch({ control, name: "terminals" });

  const connectorDirectionOptions = getOptionsFromEnum<Direction>(Direction);

  const directionOptions = (terminalId: string | undefined) => {
    if (!terminalId) return connectorDirectionOptions;

    const terminal = terminals.find((x) => x.id === terminalId);

    if (terminal?.qualifier !== Direction.Bidirectional) {
      const option = connectorDirectionOptions.find((x) => x.value === terminal?.qualifier);
      return option ? [option] : [];
    }

    return connectorDirectionOptions.filter((x) =>
      terminalTypeRefs === undefined
        ? true
        : !terminalTypeRefs
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
          onAdd={(ids) => onAddTerminals(ids, terminals, terminalFields.append, terminalTypeRefs)}
        />
      }
    >
      <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.xl}>
        {terminalFields.fields.map((field, index) => {
          return (
            <Controller
              key={field.id}
              control={control}
              name={`terminals.${index}`}
              render={({ field: { value, onChange } }) => (
                <TerminalRow
                  field={field}
                  remove={() => terminalFields.remove(index)}
                  value={value}
                  onChange={onChange}
                  directionOptions={directionOptions(field.terminal.id)}
                />
              )}
            />
          );
        })}
      </Flexbox>
    </FormSection>
  );
};

export default FormTerminals;
