import { Box, Flexbox, FormBaseFieldsContainer, FormField, Select } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { getOptionsFromEnum } from "utils";
import { AttributeFormFields } from "./AttributeForm.helpers";

interface QualifiersStepProps {
  attributeFormFields: AttributeFormFields;
  setAttributeFormFields: (nextAttributeFormFields: AttributeFormFields) => void;
}

const QualifiersStep = ({ attributeFormFields, setAttributeFormFields }: QualifiersStepProps) => {
  const theme = useTheme();

  const provenanceQualifierOptions = getOptionsFromEnum<ProvenanceQualifier>(ProvenanceQualifier);
  const rangeQualifierOptions = getOptionsFromEnum<RangeQualifier>(RangeQualifier);
  const regularityQualifierOptions = getOptionsFromEnum<RegularityQualifier>(RegularityQualifier);
  const scopeQualifierOptions = getOptionsFromEnum<ScopeQualifier>(ScopeQualifier);

  return (
    <Box maxWidth="50rem">
      <FormBaseFieldsContainer>
        <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
          <Box flexGrow="1">
            <FormField label="Provenance qualifier">
              <Select
                placeholder="Select a provenance qualifier"
                options={provenanceQualifierOptions}
                onChange={(x) => {
                  setAttributeFormFields({
                    ...attributeFormFields,
                    provenanceQualifier: x?.value ?? null,
                  });
                }}
                value={provenanceQualifierOptions.find((x) => x.value === attributeFormFields.provenanceQualifier)}
                isClearable={true}
              />
            </FormField>
          </Box>
          <Box flexGrow="1">
            <Box flexGrow="1">
              <FormField label="Range qualifier">
                <Select
                  placeholder="Select a range qualifier"
                  options={rangeQualifierOptions}
                  onChange={(x) => {
                    setAttributeFormFields({
                      ...attributeFormFields,
                      rangeQualifier: x?.value ?? null,
                    });
                  }}
                  value={rangeQualifierOptions.find((x) => x.value === attributeFormFields.rangeQualifier)}
                  isClearable={true}
                />
              </FormField>
            </Box>
          </Box>
        </Flexbox>
        <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
          <Box flexGrow="1">
            <FormField label="Regularity qualifier">
              <Select
                placeholder="Select a regularity qualifier"
                options={regularityQualifierOptions}
                onChange={(x) => {
                  setAttributeFormFields({
                    ...attributeFormFields,
                    regularityQualifier: x?.value ?? null,
                  });
                }}
                value={regularityQualifierOptions.find((x) => x.value === attributeFormFields.regularityQualifier)}
                isClearable={true}
              />
            </FormField>
          </Box>
          <Box flexGrow="1">
            <Box flexGrow="1">
              <FormField label="Scope qualifier">
                <Select
                  placeholder="Select a scope qualifier"
                  options={scopeQualifierOptions}
                  onChange={(x) => {
                    setAttributeFormFields({
                      ...attributeFormFields,
                      scopeQualifier: x?.value ?? null,
                    });
                  }}
                  value={scopeQualifierOptions.find((x) => x.value === attributeFormFields.scopeQualifier)}
                  isClearable={true}
                />
              </FormField>
            </Box>
          </Box>
        </Flexbox>
      </FormBaseFieldsContainer>
    </Box>
  );
};

export default QualifiersStep;
