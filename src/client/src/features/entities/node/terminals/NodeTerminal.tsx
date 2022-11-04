import { Aspect, ConnectorDirection } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { TerminalButton } from "common/components/terminal";
import { getValueLabelObjectsFromEnum } from "common/utils/getValueLabelObjectsFromEnum";
import { Button } from "complib/buttons";
import { FormField } from "complib/form";
import { Counter, Select } from "complib/inputs";
import { Checkbox } from "complib/inputs/checkbox/Checkbox";
import { Box, Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { useGetTerminals } from "external/sources/terminal/terminal.queries";
import { onTerminalAmountChange } from "features/entities/node/terminals/NodeTerminal.helpers";
import {
  NodeTerminalContainer,
  NodeTerminalInputContainer,
} from "features/entities/node/terminals/NodeTerminal.styled";
import { NodeTerminalAttributes } from "features/entities/node/terminals/NodeTerminalAttributes";
import { FormNodeLib } from "features/entities/node/types/formNodeLib";
import { Control, Controller, FieldArrayWithId, FieldErrors, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";

interface NodeTerminalProps {
  index: number;
  control: Control<FormNodeLib>;
  field: FieldArrayWithId<FormNodeLib, "nodeTerminals">;
  errors: FieldErrors<FormNodeLib>;
  onRemove: () => void;
}

export const NodeTerminal = ({ index, control, field, errors, onRemove }: NodeTerminalProps) => {
  const theme = useTheme();
  const { t } = useTranslation();

  const terminalQuery = useGetTerminals({ staleTime: 60 * 1000 });
  const connectorDirectionOptions = getValueLabelObjectsFromEnum<ConnectorDirection>(ConnectorDirection);

  const aspect = useWatch({ control, name: "aspect" });
  const terminalHasLimit = useWatch({ control, name: `nodeTerminals.${index}.hasMaxLimit` });
  const terminalCanHaveLimit = aspect === Aspect.Product;

  const sourceTerminal = terminalQuery.data?.find((x) => x.id === field.terminalId);

  return (
    <NodeTerminalContainer>
      <Flexbox justifyContent={"space-between"} alignItems={"center"}>
        <Text variant={"label-large"}>Terminal #{index + 1}</Text>
        <Button variant={"text"} alignSelf={"end"} icon={<Trash />} iconOnly onClick={() => onRemove()}>
          Remove terminal
        </Button>
      </Flexbox>
      <NodeTerminalInputContainer>
        <Controller
          control={control}
          name={`nodeTerminals.${index}.terminalId`}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField indent={false} label={t("terminals.name")} error={errors.nodeTerminals?.[index]?.terminalId}>
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
        <Controller
          control={control}
          name={`nodeTerminals.${index}.connectorDirection`}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField indent={false} label={t("terminals.direction")} error={errors.nodeTerminals?.[index]?.connectorDirection}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("terminals.direction").toLowerCase() })}
                options={connectorDirectionOptions}
                onChange={(x) => onChange(x?.value)}
                value={connectorDirectionOptions.find((x) => x.value === value)}
              />
            </FormField>
          )}
        />
        <NodeTerminalInputContainer>
          <Controller
            control={control}
            name={`nodeTerminals.${index}.hasMaxLimit`}
            render={({ field: { onChange, value, ...rest } }) => (
              <FormField indent={false} label={t("terminals.limit")} error={errors.nodeTerminals?.[index]?.hasMaxLimit}>
                <Box display={"flex"} justifyContent={"center"} alignItems={"center"} height={"40px"}>
                  <Checkbox {...rest} onCheckedChange={onChange} checked={value} disabled={!terminalCanHaveLimit} />
                </Box>
              </FormField>
            )}
          />
          <Controller
            control={control}
            name={`nodeTerminals.${index}.quantity`}
            render={({ field: { onChange, value, ...rest } }) => (
              <FormField indent={false} label={t("terminals.amount")} error={errors.nodeTerminals?.[index]?.quantity}>
                <Counter
                  {...rest}
                  id={field.id}
                  value={value}
                  disabled={!terminalHasLimit}
                  onChange={(val) => onTerminalAmountChange(val, onChange)}
                />
              </FormField>
            )}
          />
        </NodeTerminalInputContainer>
      </NodeTerminalInputContainer>
      <NodeTerminalAttributes attributes={sourceTerminal?.attributes ?? []} />
    </NodeTerminalContainer>
  );
};
