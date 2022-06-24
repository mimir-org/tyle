import { PlusSm } from "@styled-icons/heroicons-outline";
import { Control, Controller, useFieldArray } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import textResources from "../../../../assets/text/TextResources";
import { Button } from "../../../../complib/buttons";
import { Input, Select } from "../../../../complib/inputs";
import { Box, Flexbox, Grid } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { useGetTerminals } from "../../../../data/queries/tyle/queriesTerminal";
import { createEmptyNodeTerminalLibAm } from "../../../../models/tyle/application/nodeTerminalLibAm";
import { FormNodeLib } from "../../types/formNodeLib";
import { connectorDirectionOptions, onTerminalAmountChange } from "./NodeFormTerminals.helpers";
import { TerminalButton } from "../../../home/components/about/components/terminal/TerminalButton";
import { Fragment } from "react";
import { AttributeInfoButton } from "../../../home/components/about/components/attribute/AttributeInfoButton";
import { mapAttributeLibCmToAttributeItem } from "../../../../utils/mappers";

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
      gap={theme.tyle.spacing.xxxl}
      border={0}
      p={"0"}
    >
      <Flexbox gap={theme.tyle.spacing.xl} alignItems={"center"}>
        <Text variant={"title-large"}>{textResources.TERMINAL_TITLE}</Text>
        <Button icon={<PlusSm />} iconOnly onClick={() => terminalFields.append(createEmptyNodeTerminalLibAm())}>
          {textResources.TERMINAL_ADD}
        </Button>
      </Flexbox>

      <Grid
        gridTemplateColumns={"250px 120px 250px 1fr"}
        columnGap={theme.tyle.spacing.xxxl}
        rowGap={theme.tyle.spacing.l}
      >
        <Text variant={"title-medium"}>{textResources.TERMINAL_TABLE_NAME}</Text>
        <div />
        <Text variant={"title-medium"}>{textResources.TERMINAL_TABLE_DIRECTION}</Text>
        <Text variant={"title-medium"} pl={theme.tyle.spacing.xxxl}>
          {textResources.TERMINAL_TABLE_ATTRIBUTES}
        </Text>

        {terminalFields.fields.map((field, index) => {
          const targetTerminal = terminalQuery.data?.find((x) => x.id === terminalFields.fields[index].terminalId);

          return (
            <Fragment key={index}>
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
                    formatOptionLabel={(x) => (
                      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
                        {x.color && <TerminalButton as={"span"} variant={"small"} {...x} />}
                        <Text>{x.name}</Text>
                      </Flexbox>
                    )}
                  />
                )}
              />
              <Controller
                control={control}
                name={`nodeTerminals.${index}.quantity`}
                render={({ field: { onChange, value } }) => (
                  <Input
                    {...field}
                    type="number"
                    value={value}
                    onChange={(e) => onTerminalAmountChange(index, e, terminalFields.remove, onChange)}
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
              <Box display={"flex"} flexWrap={"wrap"} gap={theme.tyle.spacing.xl} pl={theme.tyle.spacing.xxxl}>
                {targetTerminal?.attributes.map(
                  (x) => x && <AttributeInfoButton key={x.id} {...mapAttributeLibCmToAttributeItem(x)} />
                )}
              </Box>
            </Fragment>
          );
        })}
      </Grid>
    </Box>
  );
};
