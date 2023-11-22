import { Box, Flexbox, FormField, Input } from "@mimirorg/component-library";
import { PlusCircle, XCircle } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { ValueListItem } from "./ValueConstraintStep.helpers";

interface ValueAndPatternFieldsProps {
  value: string;
  setValue: (nextValue: string) => void;
}

export const SpecificStringOrNumericalValueFields = ({ value, setValue }: ValueAndPatternFieldsProps) => {
  return (
    <Box>
      <FormField label="Value">
        <Input placeholder="Value" required={true} value={value} onChange={(event) => setValue(event.target.value)} />
      </FormField>
    </Box>
  );
};

export const SpecificBooleanValueFields = ({ value, setValue }: ValueAndPatternFieldsProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.l}>
      <Box>
        <input
          type="radio"
          value="true"
          checked={value === "true"}
          onChange={(event) => setValue(event.target.value)}
        />{" "}
        True
      </Box>
      <Box>
        <input
          type="radio"
          value="false"
          checked={value === "false"}
          onChange={(event) => setValue(event.target.value)}
        />{" "}
        False
      </Box>
    </Flexbox>
  );
};

interface ValueListFieldsProps {
  valueList: ValueListItem[];
  setValueList: (nextValueList: ValueListItem[]) => void;
}

export const ValueListFields = ({ valueList, setValueList }: ValueListFieldsProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.l}>
      {valueList.map((item, index) => (
        <Flexbox key={item.id} justifyContent="space-between" alignItems="center" gap={theme.mimirorg.spacing.l}>
          <Box flexGrow="1">
            <Input
              value={item.value}
              onChange={(event) => {
                const nextValueList = [...valueList];
                nextValueList[index] = { id: valueList[index].id, value: event.target.value };
                setValueList(nextValueList);
              }}
            />
          </Box>
          <XCircle
            size="1.2rem"
            color={theme.mimirorg.color.dangerousAction.on}
            style={{ cursor: "pointer" }}
            onClick={() => {
              setValueList(valueList.filter((x) => x.id !== item.id));
            }}
          />
        </Flexbox>
      ))}
      <Flexbox justifyContent="center">
        <PlusCircle
          color={theme.mimirorg.color.primary.base}
          size="2rem"
          style={{ cursor: "pointer" }}
          onClick={() => setValueList([...valueList, { id: crypto.randomUUID(), value: "" }])}
        />
      </Flexbox>
    </Flexbox>
  );
};

export const PatternFields = ({ value, setValue }: ValueAndPatternFieldsProps) => {
  return (
    <Box>
      <FormField label="Pattern">
        <Input placeholder="Pattern" required={true} value={value} onChange={(event) => setValue(event.target.value)} />
      </FormField>
    </Box>
  );
};

interface NumberRangeFieldsProps {
  minValue: string;
  maxValue: string;
  setNumberRange: (min: string, max: string) => void;
}

export const NumberRangeFields = ({ minValue, maxValue, setNumberRange }: NumberRangeFieldsProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
      <Box flexGrow="1">
        <FormField label="Lower bound">
          <Input
            placeholder="Lower bound"
            value={minValue}
            onChange={(event) => setNumberRange(event.target.value, maxValue)}
          />
        </FormField>
      </Box>
      <Box flexGrow="1">
        <FormField label="Upper bound">
          <Input
            placeholder="Upper bound"
            value={maxValue}
            onChange={(event) => setNumberRange(minValue, event.target.value)}
          />
        </FormField>
      </Box>
    </Flexbox>
  );
};
