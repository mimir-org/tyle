import { Aspect, ConnectorDirection } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import {
  MAXIMUM_TERMINAL_QUANTITY_VALUE,
  MINIMUM_TERMINAL_QUANTITY_VALUE,
} from "common/utils/aspectObjectTerminalQuantityRestrictions";
import { Button } from "complib/buttons";
import { FormField } from "complib/form";
import { Counter } from "complib/inputs";
import { Box, Checkbox, Flexbox, Select, Text } from "@mimirorg/component-library";
import { useGetTerminals } from "external/sources/terminal/terminal.queries";
import { TerminalButton } from "features/common/terminal";
import {
  AspectObjectTerminalContainer,
  AspectObjectTerminalInputContainer,
} from "features/entities/aspectobject/terminals/AspectObjectTerminal.styled";
import { AspectObjectTerminalAttributes } from "features/entities/aspectobject/terminals/AspectObjectTerminalAttributes";
import { FormAspectObjectLib } from "features/entities/aspectobject/types/formAspectObjectLib";
import { Control, Controller, FieldArrayWithId, FieldErrors, UseFormSetValue, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Accordion, AccordionContent, AccordionItem, AccordionTrigger } from "../../../../complib/surfaces";
import { useEffect } from "react";

interface AspectObjectTerminalProps {
  index: number;
  control: Control<FormAspectObjectLib>;
  field: FieldArrayWithId<FormAspectObjectLib, "aspectObjectTerminals">;
  errors: FieldErrors<FormAspectObjectLib>;
  setValue: UseFormSetValue<FormAspectObjectLib>;
  removable: boolean;
  onRemove: () => void;
  minValue?: number;
}

/**
 * Component which represents a single terminal for a given aspect object.
 * Displays the various input fields that the terminal model supports.
 *
 * @param index
 * @param control
 * @param field
 * @param errors
 * @param setValue
 * @param removable
 * @param onRemove
 * @constructor
 */
export const AspectObjectTerminal = ({
  index,
  control,
  field,
  errors,
  setValue,
  removable = true,
  onRemove,
  minValue,
}: AspectObjectTerminalProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const terminalQuery = useGetTerminals({ staleTime: 60 * 1000 });
  const connectorDirectionOptions = getOptionsFromEnum<ConnectorDirection>(ConnectorDirection);

  const aspect = useWatch({ control, name: "aspect" });
  const allTerminals = useWatch({ control, name: "aspectObjectTerminals" });
  const terminalHasMaxQuantity = useWatch({ control, name: `aspectObjectTerminals.${index}.hasMaxQuantity` });
  const terminalCanHaveLimit = aspect === Aspect.Product;

  const directionOptions = (terminalId: string | undefined) => {
    if (!terminalId) return connectorDirectionOptions;

    return connectorDirectionOptions.filter(
      (x) =>
        !allTerminals
          .filter((y) => y.terminalId === terminalId)
          .map((y) => y.connectorDirection)
          .includes(x.value),
    );
  };

  const sourceTerminal = terminalQuery.data?.find((x) => x.id === allTerminals[index].terminalId);

  useEffect(() => {
    if (aspect === Aspect.Function) {
      setValue(`aspectObjectTerminals.${index}.maxQuantity`, 0, {
        shouldDirty: true,
      });
      setValue(`aspectObjectTerminals.${index}.hasMaxQuantity`, false, {
        shouldDirty: true,
      });
    }
  }, [index, setValue, aspect]);

  return (
    <Flexbox gap={"24px"} alignItems={"center"}>
      <Text variant={"title-large"}>{index + 1}</Text>
      <AspectObjectTerminalContainer>
        <AspectObjectTerminalInputContainer>
          <Controller
            control={control}
            name={`aspectObjectTerminals.${index}.terminalId`}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <FormField
                indent={false}
                label={t("aspectObject.terminals.name")}
                error={errors.aspectObjectTerminals?.[index]?.terminalId}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("aspectObject.terminals.name").toLowerCase() })}
                  options={terminalQuery.data?.filter(
                    (x) => allTerminals.filter((y) => y.terminalId === x.id).length < connectorDirectionOptions.length,
                  )}
                  isLoading={terminalQuery.isLoading}
                  getOptionLabel={(x) => x.name}
                  getOptionValue={(x) => x.id.toString()}
                  onChange={(x) => {
                    onChange(x?.id);
                    setValue(`aspectObjectTerminals.${index}.connectorDirection`, directionOptions(x?.id)[0].value);
                  }}
                  value={terminalQuery.data?.find((x) => x.id === value)}
                  formatOptionLabel={(x) => (
                    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
                      {x.color && <TerminalButton as={"span"} variant={"small"} color={x.color} />}
                      <Text>{x.name}</Text>
                    </Flexbox>
                  )}
                />
              </FormField>
            )}
          />
          <Controller
            control={control}
            name={`aspectObjectTerminals.${index}.connectorDirection`}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <FormField
                indent={false}
                label={t("aspectObject.terminals.direction")}
                error={errors.aspectObjectTerminals?.[index]?.connectorDirection}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", {
                    object: t("aspectObject.terminals.direction").toLowerCase(),
                  })}
                  options={directionOptions(allTerminals[index].terminalId)}
                  onChange={(x) => onChange(x?.value)}
                  value={connectorDirectionOptions.find((x) => x.value === value)}
                  isDisabled={!removable}
                />
              </FormField>
            )}
          />
          {terminalCanHaveLimit && (
            <AspectObjectTerminalInputContainer>
              <Controller
                control={control}
                name={`aspectObjectTerminals.${index}.hasMaxQuantity`}
                render={({ field: { onChange, value, ...rest } }) => (
                  <FormField
                    indent={false}
                    label={t("aspectObject.terminals.limit")}
                    error={errors.aspectObjectTerminals?.[index]?.hasMaxQuantity}
                  >
                    <Box display={"flex"} justifyContent={"center"} alignItems={"center"} height={"40px"}>
                      <Checkbox
                        {...rest}
                        onCheckedChange={(checked) => {
                          !checked &&
                            setValue(`aspectObjectTerminals.${index}.maxQuantity`, 0, {
                              shouldDirty: true,
                            });
                          checked &&
                            setValue(`aspectObjectTerminals.${index}.maxQuantity`, minValue ?? 1, {
                              shouldDirty: true,
                            });
                          onChange(checked);
                        }}
                        checked={value}
                        disabled={!terminalCanHaveLimit}
                      />
                    </Box>
                  </FormField>
                )}
              />
              <Controller
                control={control}
                name={`aspectObjectTerminals.${index}.maxQuantity`}
                render={({ field: { value, ...rest } }) => (
                  <FormField
                    indent={false}
                    label={t("aspectObject.terminals.amount")}
                    error={errors.aspectObjectTerminals?.[index]?.maxQuantity}
                  >
                    <Counter
                      {...rest}
                      id={field.id}
                      min={minValue ?? MINIMUM_TERMINAL_QUANTITY_VALUE}
                      max={MAXIMUM_TERMINAL_QUANTITY_VALUE}
                      value={!terminalHasMaxQuantity ? 0 : value}
                      disabled={!terminalHasMaxQuantity}
                    />
                  </FormField>
                )}
              />
            </AspectObjectTerminalInputContainer>
          )}
        </AspectObjectTerminalInputContainer>
        {sourceTerminal && sourceTerminal.attributes.length >= 4 ? (
          <Accordion>
            <AccordionItem value={"attributes"}>
              <AccordionTrigger>{t("aspectObject.terminals.attributes")}</AccordionTrigger>
              <AccordionContent>
                <AspectObjectTerminalAttributes hideLabel attributes={sourceTerminal?.attributes ?? []} />
              </AccordionContent>
            </AccordionItem>
          </Accordion>
        ) : (
          <AspectObjectTerminalAttributes hideLabel attributes={sourceTerminal?.attributes ?? []} />
        )}
      </AspectObjectTerminalContainer>
      <Box>
        <Button variant={"outlined"} dangerousAction disabled={!removable} alignSelf={"end"} onClick={() => onRemove()}>
          <Trash size={48} />
        </Button>
      </Box>
    </Flexbox>
  );
};
