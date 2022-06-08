import { PlusSm } from "@styled-icons/heroicons-outline";
import { Control, Controller, useFieldArray } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import textResources from "../../../../assets/text/TextResources";
import { Button } from "../../../../complib/buttons";
import { Input, Select } from "../../../../complib/inputs";
import { Box, Flexbox } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { useGetTerminals } from "../../../../data/queries/tyle/queriesTerminal";
import { createEmptyNodeTerminalLibAm } from "../../../../models/tyle/application/nodeTerminalLibAm";
import { FormNodeLib } from "../../types/formNodeLib";
import { connectorDirectionOptions, onTerminalAmountChange } from "./NodeFormTerminals.helpers";

export interface NodeFormTerminalsProps {
  control: Control<FormNodeLib>;
}

export const NodeFormTerminals = ({ control }: NodeFormTerminalsProps) => {
  const theme = useTheme();
  const terminalQuery = useGetTerminals();
  const terminalFields = useFieldArray({ control, name: "nodeTerminals" });

  return (
    <Box
      as={"fieldset"}
      display={"flex"}
      flexDirection={"column"}
      justifyContent={"center"}
      gap={theme.tyle.spacing.medium}
      border={0}
      p={"0"}
    >
      <Flexbox gap={theme.tyle.spacing.medium} justifyContent={"space-between"}>
        <Text variant={"headline-medium"}>{textResources.TERMINAL_TITLE}</Text>
        <Button icon={<PlusSm />} iconOnly onClick={() => terminalFields.append(createEmptyNodeTerminalLibAm())}>
          {textResources.TERMINAL_ADD}
        </Button>
      </Flexbox>

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.medium}>
        {terminalFields.fields.map((field, index) => (
          <Flexbox key={field.id} gap={theme.tyle.spacing.small} flexWrap={"wrap"}>
            <Controller
              control={control}
              name={`nodeTerminals.${index}.terminalId`}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={textResources.TERMINAL_PLACEHOLDER}
                  options={terminalQuery.data}
                  isLoading={terminalQuery.isLoading}
                  getOptionLabel={(x) => x.name}
                  getOptionValue={(x) => x.id}
                  onChange={(x) => onChange(x?.id)}
                  value={terminalQuery.data?.find((x) => x.id === value)}
                />
              )}
            />
            <Controller
              control={control}
              name={`nodeTerminals.${index}.connectorDirection`}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={textResources.TERMINAL_DIRECTION_PLACEHOLDER}
                  options={connectorDirectionOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => onChange(x?.value)}
                  value={connectorDirectionOptions.find((x) => x.value === value)}
                />
              )}
            />
            <Controller
              control={control}
              name={`nodeTerminals.${index}.number`}
              render={({ field: { onChange, value } }) => (
                <Input
                  {...field}
                  type="number"
                  value={value}
                  onChange={(e) => onTerminalAmountChange(index, e, terminalFields.remove, onChange)}
                />
              )}
            />
          </Flexbox>
        ))}
      </Flexbox>
    </Box>
  );
};
