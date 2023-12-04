import { FormField, Select } from "@mimirorg/component-library";
import React from "react";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { getOptionsFromEnum } from "utils";
import { AttributeFormStepProps } from "./AttributeForm";
import { QualifiersFormWrapper } from "./QualifiersForm.styled";

const QualifiersForm = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields, setFields }, ref) => {
  const { provenanceQualifier, rangeQualifier, regularityQualifier, scopeQualifier } = fields;
  const setProvenanceQualifier = (provenanceQualifier: ProvenanceQualifier | undefined) =>
    setFields({ ...fields, provenanceQualifier: provenanceQualifier ?? null });
  const setRangeQualifier = (rangeQualifier: RangeQualifier | undefined) =>
    setFields({ ...fields, rangeQualifier: rangeQualifier ?? null });
  const setRegularityQualifier = (regularityQualifier: RegularityQualifier | undefined) =>
    setFields({ ...fields, regularityQualifier: regularityQualifier ?? null });
  const setScopeQualifier = (scopeQualifier: ScopeQualifier | undefined) =>
    setFields({ ...fields, scopeQualifier: scopeQualifier ?? null });

  const provenanceQualifierOptions = getOptionsFromEnum<ProvenanceQualifier>(ProvenanceQualifier);
  const rangeQualifierOptions = getOptionsFromEnum<RangeQualifier>(RangeQualifier);
  const regularityQualifierOptions = getOptionsFromEnum<RegularityQualifier>(RegularityQualifier);
  const scopeQualifierOptions = getOptionsFromEnum<ScopeQualifier>(ScopeQualifier);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  return (
    <QualifiersFormWrapper onSubmit={handleSubmit} ref={ref}>
      <FormField label="Provenance qualifier">
        <Select
          options={provenanceQualifierOptions}
          onChange={(x) => setProvenanceQualifier(x?.value)}
          value={provenanceQualifierOptions.find((x) => x.value === provenanceQualifier)}
          isClearable={true}
        />
      </FormField>
      <FormField label="Range qualifier">
        <Select
          options={rangeQualifierOptions}
          onChange={(x) => setRangeQualifier(x?.value)}
          value={rangeQualifierOptions.find((x) => x.value === rangeQualifier)}
          isClearable={true}
        />
      </FormField>
      <FormField label="Regularity qualifier">
        <Select
          options={regularityQualifierOptions}
          onChange={(x) => setRegularityQualifier(x?.value)}
          value={regularityQualifierOptions.find((x) => x.value === regularityQualifier)}
          isClearable={true}
        />
      </FormField>
      <FormField label="Scope qualifier">
        <Select
          options={scopeQualifierOptions}
          onChange={(x) => setScopeQualifier(x?.value)}
          value={scopeQualifierOptions.find((x) => x.value === scopeQualifier)}
          isClearable={true}
        />
      </FormField>
    </QualifiersFormWrapper>
  );
});

QualifiersForm.displayName = "QualifiersForm";

export default QualifiersForm;
