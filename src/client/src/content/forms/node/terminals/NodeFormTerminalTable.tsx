import { ConnectorDirection } from "@mimirorg/typelibrary-types";
import { PlusSm } from "@styled-icons/heroicons-outline";
import { Control, Controller, useFieldArray } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import textResources from "../../../../assets/text/TextResources";
import { Button } from "../../../../complib/buttons";
import { Table, Tbody, Td, Th, Thead, Tr } from "../../../../complib/data-display";
import { Input, Select } from "../../../../complib/inputs";
import { Box, Flexbox } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { useGetTerminals } from "../../../../data/queries/tyle/queriesTerminal";
import { useMediaQuery } from "../../../../hooks/useMediaQuery";
import { createEmptyNodeTerminalLibAm } from "../../../../models/tyle/application/nodeTerminalLibAm";
import { getValueLabelObjectsFromEnum } from "../../../../utils/getValueLabelObjectsFromEnum";
import { mapAttributeLibCmToAttributeItem } from "../../../../utils/mappers";
import { AttributeInfoButton } from "../../../common/attribute";
import { TerminalButton } from "../../../common/terminal";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormSection } from "../NodeFormSection";
import { onTerminalAmountChange } from "./NodeFormTerminalTable.helpers";

export interface NodeFormTerminalsProps {
  control: Control<FormNodeLib>;
}

export const NodeFormTerminalTable = ({ control }: NodeFormTerminalsProps) => {
  const theme = useTheme();
  const terminalQuery = useGetTerminals();
  const terminalFields = useFieldArray({ control, name: "nodeTerminals" });
  const adjustAttributesPadding = useMediaQuery("screen and (min-width: 1500px)");
  const connectorDirectionOptions = getValueLabelObjectsFromEnum<ConnectorDirection>(ConnectorDirection);

  return (
    <NodeFormSection
      title={textResources.TERMINAL_TITLE}
      action={
        <Button icon={<PlusSm />} iconOnly onClick={() => terminalFields.append(createEmptyNodeTerminalLibAm())}>
          {textResources.TERMINAL_ADD}
        </Button>
      }
    >
      <Table>
        <Thead>
          <Tr>
            <Th>
              <Text as={"span"} variant={"title-medium"}>
                {textResources.TERMINAL_TABLE_NAME}
              </Text>
            </Th>
            <Th>
              <Text as={"span"} variant={"title-medium"}>
                {textResources.TERMINAL_TABLE_AMOUNT}
              </Text>
            </Th>
            <Th>
              <Text as={"span"} variant={"title-medium"}>
                {textResources.TERMINAL_TABLE_DIRECTION}
              </Text>
            </Th>
            <Th>
              <Text
                as={"span"}
                variant={"title-medium"}
                pl={adjustAttributesPadding ? theme.tyle.spacing.xxxl : undefined}
              >
                {textResources.TERMINAL_TABLE_ATTRIBUTES}
              </Text>
            </Th>
          </Tr>
        </Thead>
        <Tbody>
          {terminalFields.fields.map((field, index) => {
            const targetTerminal = terminalQuery.data?.find((x) => x.id === terminalFields.fields[index].terminalId);

            return (
              <Tr key={field.id}>
                <Td data-label={textResources.TERMINAL_TABLE_NAME}>
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
                            {x.color && <TerminalButton as={"span"} variant={"small"} color={x.color} />}
                            <Text>{x.name}</Text>
                          </Flexbox>
                        )}
                      />
                    )}
                  />
                </Td>
                <Td data-label={textResources.TERMINAL_TABLE_AMOUNT}>
                  <Controller
                    control={control}
                    name={`nodeTerminals.${index}.quantity`}
                    render={({ field: { onChange, value } }) => (
                      <Input
                        {...field}
                        type="number"
                        value={value}
                        onChange={(e) => onTerminalAmountChange(index, e, terminalFields.remove, onChange)}
                        maxWidth={"120px"}
                      />
                    )}
                  />
                </Td>
                <Td data-label={textResources.TERMINAL_TABLE_DIRECTION}>
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
                </Td>
                <Td data-label={textResources.TERMINAL_TABLE_ATTRIBUTES}>
                  <Box
                    display={"flex"}
                    flexWrap={"wrap"}
                    gap={theme.tyle.spacing.base}
                    pl={adjustAttributesPadding ? theme.tyle.spacing.xxxl : undefined}
                  >
                    {targetTerminal?.attributes.map(
                      (x) => x && <AttributeInfoButton key={x.id} {...mapAttributeLibCmToAttributeItem(x)} />
                    )}
                  </Box>
                </Td>
              </Tr>
            );
          })}
        </Tbody>
      </Table>
    </NodeFormSection>
  );
};
