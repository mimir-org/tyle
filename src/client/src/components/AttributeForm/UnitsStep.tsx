import { Box, Flexbox, FormField, Select, Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetUnits } from "api/unit.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { useTheme } from "styled-components";
import { RdlUnit } from "types/attributes/rdlUnit";
import { InfoItem } from "types/infoItem";
import { getOptionsFromEnum } from "utils";
import { UnitRequirement } from "./AttributeForm.helpers";

interface UnitsStepProps {
  unitRequirement: UnitRequirement;
  setUnitRequirement: (nextUnitRequirement: UnitRequirement) => void;
  units: RdlUnit[];
  setUnits: (nextUnits: RdlUnit[]) => void;
}

const UnitsStep = ({ unitRequirement, setUnitRequirement, units, setUnits }: UnitsStepProps) => {
  const theme = useTheme();

  const unitRequirementOptions = getOptionsFromEnum<UnitRequirement>(UnitRequirement);

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

  const handleRemoveUnit = (id: number) => {
    const nextUnits = units.filter((unit) => unit.id !== id);
    setUnits(nextUnits);
  };

  return (
    <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.xl}>
      <Box maxWidth="20rem">
        <FormField label="Unit requirement">
          <Select
            options={unitRequirementOptions}
            onChange={(x) => {
              setUnitRequirement(x?.value ?? UnitRequirement.NoUnit);
            }}
            value={unitRequirementOptions.find((x) => x.value === unitRequirement)}
          />
        </FormField>
      </Box>
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
              setUnits([...units, ...unitsToAdd]);
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
            onAction={() => handleRemoveUnit(unit.id)}
            dangerousAction
          >
            {unit.name}
          </Token>
        ))}
      </FormSection>
    </Flexbox>
  );
};

export default UnitsStep;
