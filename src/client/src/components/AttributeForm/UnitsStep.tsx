import { Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetUnits } from "api/unit.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import React from "react";
import { RdlUnit } from "types/attributes/rdlUnit";
import { InfoItem } from "types/infoItem";
import { AttributeFormStepProps } from "./AttributeForm";
import { UnitRequirement } from "./AttributeForm.helpers";
import { UnitRequirementFieldset, UnitRequirementLegend, UnitsStepWrapper } from "./UnitsStep.styled";

const UnitsStep = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields, setFields }, ref) => {
  const { unitRequirement, units } = fields;
  const setUnitRequirement = (unitRequirement: UnitRequirement) => setFields({ ...fields, unitRequirement });
  const addUnits = (unitsToAdd: RdlUnit[]) => setFields({ ...fields, units: [...fields.units, ...unitsToAdd] });
  const removeUnit = (unitToRemove: RdlUnit) =>
    setFields({ ...fields, units: fields.units.filter((unit) => unit.id !== unitToRemove.id) });

  const unitQuery = useGetUnits();
  const unitInfoItems: InfoItem[] =
    unitQuery.data?.map((unit) => ({
      id: unit.id.toString(),
      name: unit.name + (unit.symbol ? ` (${unit.symbol})` : ""),
      descriptors: {
        Description: unit.description,
        IRI: unit.iri,
      },
    })) ?? [];

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  return (
    <UnitsStepWrapper onSubmit={handleSubmit} ref={ref}>
      <UnitRequirementFieldset>
        <UnitRequirementLegend>Unit requirement</UnitRequirementLegend>
        <label htmlFor="no-unit">
          <input
            type="radio"
            id="no-unit"
            name="unit-requirement"
            value={UnitRequirement.NoUnit}
            checked={unitRequirement === UnitRequirement.NoUnit}
            onChange={() => setUnitRequirement(UnitRequirement.NoUnit)}
          />{" "}
          No unit
        </label>
        <label htmlFor="unit-optional">
          <input
            type="radio"
            id="unit-optional"
            name="unit-requirement"
            value={UnitRequirement.Optional}
            checked={unitRequirement === UnitRequirement.Optional}
            onChange={() => setUnitRequirement(UnitRequirement.Optional)}
          />{" "}
          Optional
        </label>
        <label htmlFor="unit-required">
          <input
            type="radio"
            id="unit-required"
            name="unit-requirement"
            value={UnitRequirement.Required}
            checked={unitRequirement === UnitRequirement.Required}
            onChange={() => setUnitRequirement(UnitRequirement.Required)}
          />{" "}
          Required
        </label>
      </UnitRequirementFieldset>
      <FormSection
        title="Add units"
        action={
          <SelectItemDialog
            title="Select units"
            description="You can select one or more units"
            searchFieldText="Search"
            addItemsButtonText="Add units"
            openDialogButtonText="Open add units dialog"
            items={unitInfoItems.filter(
              (unit) => units.filter((chosen) => chosen.id.toString() === unit.id).length === 0,
            )}
            onAdd={(ids) => {
              const unitsToAdd: RdlUnit[] = [];
              ids.forEach((id) => {
                const targetUnit = unitQuery.data?.find((x) => x.id === Number(id));
                if (targetUnit) unitsToAdd.push(targetUnit);
              });
              addUnits(unitsToAdd);
            }}
          />
        }
      >
        {units.map((unit) => (
          <Token
            variant="secondary"
            key={unit.id}
            actionable
            actionIcon={<XCircle />}
            actionText="Remove unit"
            onAction={() => removeUnit(unit)}
            dangerousAction
          >
            {unit.name + (unit.symbol ? ` (${unit.symbol})` : "")}
          </Token>
        ))}
      </FormSection>
    </UnitsStepWrapper>
  );
});

UnitsStep.displayName = "UnitsStep";

export default UnitsStep;
