import { ConnectorDirection } from "@mimirorg/typelibrary-types";
import { TerminalButton } from "common/components/terminal";
import { getValueLabelObjectsFromEnum } from "common/utils/getValueLabelObjectsFromEnum";
import { Table, Tbody, Td, Thead, Tr } from "complib/data-display";
import { FormField } from "complib/form";
import { Counter, Select } from "complib/inputs";
import { Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { Controller, useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { useGetTerminals } from "../../../../data/queries/tyle/queriesTerminal";
import { FormSection } from "../../common/form-section/FormSection";
import { FormNodeLib } from "../types/formNodeLib";
import { createEmptyNodeTerminalLibAm, onTerminalAmountChange } from "./NodeFormTerminalTable.helpers";
import { NodeFormTerminalTableAddButton } from "./NodeFormTerminalTableAddButton";
import { NodeFormTerminalTableAttributes } from "./NodeFormTerminalTableAttributes";
import { NodeFormTerminalTableHeader } from "./NodeFormTerminalTableHeader";

export const NodeFormTerminalTable = () => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { control, formState } = useFormContext<FormNodeLib>();
  const { errors } = formState;

  const terminalQuery = useGetTerminals();
  const terminalFields = useFieldArray({ control, name: "nodeTerminals" });
  const connectorDirectionOptions = getValueLabelObjectsFromEnum<ConnectorDirection>(ConnectorDirection);

  return (
    <FormSection
      title={t("terminals.title")}
      action={<NodeFormTerminalTableAddButton onClick={() => terminalFields.append(createEmptyNodeTerminalLibAm())} />}
    >
      <Table>
        <Thead>
          <NodeFormTerminalTableHeader />
        </Thead>
        <Tbody>
          {terminalFields.fields.map((field, index) => {
            const targetTerminal = terminalQuery.data?.find((x) => x.id === terminalFields.fields[index].terminalId);

            return (
              <Tr key={field.id}>
                <Td data-label={t("terminals.templates.terminal", { object: t("terminals.name").toLowerCase() })}>
                  <Controller
                    control={control}
                    name={`nodeTerminals.${index}.terminalId`}
                    render={({ field: { value, onChange, ref, ...rest } }) => (
                      <FormField error={errors.nodeTerminals?.[index]?.terminalId}>
                        <Select
                          {...rest}
                          selectRef={ref}
                          placeholder={t("common.templates.select", { object: t("terminals.name").toLowerCase() })}
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
                      </FormField>
                    )}
                  />
                </Td>
                <Td data-label={t("terminals.amount")}>
                  <Controller
                    control={control}
                    name={`nodeTerminals.${index}.quantity`}
                    render={({ field: { onChange, value, ...rest } }) => (
                      <Counter
                        {...rest}
                        id={field.id}
                        value={value}
                        onChange={(val) => onTerminalAmountChange(index, val, terminalFields.remove, onChange)}
                      />
                    )}
                  />
                </Td>
                <Td data-label={t("terminals.templates.terminal", { object: t("terminals.direction").toLowerCase() })}>
                  <Controller
                    control={control}
                    name={`nodeTerminals.${index}.connectorDirection`}
                    render={({ field: { value, onChange, ref, ...rest } }) => (
                      <Select
                        {...rest}
                        selectRef={ref}
                        placeholder={t("common.templates.select", { object: t("terminals.direction").toLowerCase() })}
                        options={connectorDirectionOptions}
                        onChange={(x) => onChange(x?.value)}
                        value={connectorDirectionOptions.find((x) => x.value === value)}
                      />
                    )}
                  />
                </Td>
                <NodeFormTerminalTableAttributes attributes={targetTerminal?.attributes ?? []} />
              </Tr>
            );
          })}
        </Tbody>
      </Table>
    </FormSection>
  );
};
