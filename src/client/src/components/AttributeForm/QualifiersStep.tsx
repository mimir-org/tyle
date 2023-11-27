import { Box, Flexbox, FormBaseFieldsContainer, FormField, Select } from "@mimirorg/component-library";
import React from "react";
import { useTheme } from "styled-components";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { getOptionsFromEnum } from "utils";
import { AttributeFormStepProps } from "./AttributeForm";

const QualifiersStep = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields, setFields }, ref) => {
  const theme = useTheme();

  const [provenanceQualifier, setProvenanceQualifier] = React.useState(fields.provenanceQualifier);
  const [rangeQualifier, setRangeQualifier] = React.useState(fields.rangeQualifier);
  const [regularityQualifier, setRegularityQualifier] = React.useState(fields.regularityQualifier);
  const [scopeQualifier, setScopeQualifier] = React.useState(fields.scopeQualifier);

  const provenanceQualifierOptions = getOptionsFromEnum<ProvenanceQualifier>(ProvenanceQualifier);
  const rangeQualifierOptions = getOptionsFromEnum<RangeQualifier>(RangeQualifier);
  const regularityQualifierOptions = getOptionsFromEnum<RegularityQualifier>(RegularityQualifier);
  const scopeQualifierOptions = getOptionsFromEnum<ScopeQualifier>(ScopeQualifier);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setFields({ ...fields, provenanceQualifier, rangeQualifier, regularityQualifier, scopeQualifier });
  };

  return (
    <form onSubmit={handleSubmit} ref={ref}>
      <Box maxWidth="50rem">
        <FormBaseFieldsContainer>
          <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
            <Box flexGrow="1">
              <FormField label="Provenance qualifier">
                <Select
                  placeholder="Select a provenance qualifier"
                  options={provenanceQualifierOptions}
                  onChange={(x) => setProvenanceQualifier(x?.value ?? null)}
                  value={provenanceQualifierOptions.find((x) => x.value === provenanceQualifier)}
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
                    onChange={(x) => setRangeQualifier(x?.value ?? null)}
                    value={rangeQualifierOptions.find((x) => x.value === rangeQualifier)}
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
                  onChange={(x) => setRegularityQualifier(x?.value ?? null)}
                  value={regularityQualifierOptions.find((x) => x.value === regularityQualifier)}
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
                    onChange={(x) => setScopeQualifier(x?.value ?? null)}
                    value={scopeQualifierOptions.find((x) => x.value === scopeQualifier)}
                    isClearable={true}
                  />
                </FormField>
              </Box>
            </Box>
          </Flexbox>
        </FormBaseFieldsContainer>
      </Box>
    </form>
  );
});

QualifiersStep.displayName = "QualifiersStep";

export default QualifiersStep;
