import { Box, Flexbox, FormBaseFieldsContainer, FormField, Select } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { getOptionsFromEnum } from "utils";
import { AttributeQualifierFields } from "./AttributeForm.helpers";

interface QualifiersStepProps {
  qualifiers: AttributeQualifierFields;
  setQualifiers: (nextQualifiers: AttributeQualifierFields) => void;
}

const QualifiersStep = ({ qualifiers, setQualifiers }: QualifiersStepProps) => {
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
                  setQualifiers({
                    ...qualifiers,
                    provenance: x?.value ?? null,
                  });
                }}
                value={provenanceQualifierOptions.find((x) => x.value === qualifiers.provenance)}
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
                    setQualifiers({
                      ...qualifiers,
                      range: x?.value ?? null,
                    });
                  }}
                  value={rangeQualifierOptions.find((x) => x.value === qualifiers.range)}
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
                  setQualifiers({
                    ...qualifiers,
                    regularity: x?.value ?? null,
                  });
                }}
                value={regularityQualifierOptions.find((x) => x.value === qualifiers.regularity)}
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
                    setQualifiers({
                      ...qualifiers,
                      scope: x?.value ?? null,
                    });
                  }}
                  value={scopeQualifierOptions.find((x) => x.value === qualifiers.scope)}
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
