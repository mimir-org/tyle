import { Box, Flexbox, FormField, Select } from "@mimirorg/component-library";
import Switch from "components/Switch";
import { useTheme } from "styled-components";
import { ConstraintType } from "types/attributes/constraintType";
import { ValueConstraintRequest } from "types/attributes/valueConstraintRequest";
import { XsdDataType } from "types/attributes/xsdDataType";
import { getOptionsFromEnum } from "utils";

interface ValueConstraintStepProps {
  valueConstraint: ValueConstraintRequest | null;
  setValueConstraint: (nextValueConstraint: ValueConstraintRequest | null) => void;
}

const ValueConstraintStep = ({ valueConstraint, setValueConstraint }: ValueConstraintStepProps) => {
  const theme = useTheme();

  const constraintTypeOptions = getOptionsFromEnum<ConstraintType>(ConstraintType);
  const dataTypeOptions = getOptionsFromEnum<XsdDataType>(XsdDataType);

  const handleChange = (checked: boolean) => {
    if (checked) {
      setValueConstraint({
        constraintType: ConstraintType.HasSpecificDataType,
        dataType: XsdDataType.String,
        value: null,
        valueList: null,
        pattern: null,
        minValue: null,
        maxValue: null,
        minCount: 1,
        maxCount: 1,
      });
    } else {
      setValueConstraint(null);
    }
  };

  return (
    <Box maxWidth="50rem">
      <Flexbox gap={theme.mimirorg.spacing.xl} flexDirection="column">
        <Flexbox alignItems="center" gap={theme.mimirorg.spacing.l}>
          <Box>Add value constraint</Box>
          <Switch checked={valueConstraint !== null} onCheckedChange={(checked) => handleChange(checked)} />
        </Flexbox>
        {valueConstraint && (
          <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
            <Box flexGrow="1">
              <FormField label="Constraint type">
                <Select
                  placeholder="Select a constraint type"
                  options={constraintTypeOptions}
                  onChange={(x) => {
                    if (valueConstraint !== null) {
                      setValueConstraint({
                        ...valueConstraint,
                        constraintType: x?.value ?? ConstraintType.HasSpecificValue,
                      });
                    }
                  }}
                  value={constraintTypeOptions.find((x) => x.value === valueConstraint?.constraintType)}
                />
              </FormField>
            </Box>
            <Box flexGrow="1">
              <Box flexGrow="1">
                <FormField label="Data type">
                  <Select
                    placeholder="Select a data type"
                    options={dataTypeOptions}
                    onChange={(x) => {
                      if (valueConstraint !== null) {
                        setValueConstraint({
                          ...valueConstraint,
                          dataType: x?.value ?? XsdDataType.String,
                        });
                      }
                    }}
                    value={dataTypeOptions.find((x) => x.value === valueConstraint?.dataType)}
                  />
                </FormField>
              </Box>
            </Box>
          </Flexbox>
        )}
      </Flexbox>
    </Box>
  );
};

export default ValueConstraintStep;
