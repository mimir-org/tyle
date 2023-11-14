import { Flexbox } from "@mimirorg/component-library";
import { useGetAttributes } from "api/attribute.queries";
import AttributeRow from "components/FormAttributes/AttributeRow";
import { prepareAttributes } from "components/FormAttributes/prepareAttributes";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { mapAttributeViewsToInfoItems } from "helpers/mappers.helpers";
import { useTheme } from "styled-components/macro";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";

interface AttributesStepProps {
  chosenAttributes: AttributeTypeReferenceView[];
  setAttributes: (attributes: AttributeTypeReferenceView[]) => void;
}

const AttributesStep = ({ chosenAttributes, setAttributes }: AttributesStepProps) => {
  const theme = useTheme();

  const attributeQuery = useGetAttributes();
  const attributes = prepareAttributes(attributeQuery.data) ?? [];

  const handleAttributeRowChange = (changedAttribute: AttributeTypeReferenceView) => {
    const nextAttributes = [...chosenAttributes];
    const changedAttributeIndex = nextAttributes.findIndex(
      (attribute) => attribute.attribute.id === changedAttribute.attribute.id,
    );
    if (changedAttributeIndex >= 0) {
      nextAttributes[changedAttributeIndex] = changedAttribute;
    }
    setAttributes(nextAttributes);
  };

  const handleRemove = (index: number) => {
    const nextAttributes = [...chosenAttributes];
    nextAttributes.splice(index, 1);
    setAttributes(nextAttributes);
  };

  return (
    <FormSection
      title="Add attributes"
      action={
        <SelectItemDialog
          title="Select attributes"
          description="You can select one or more attributes"
          searchFieldText="Search"
          addItemsButtonText="Add attributes"
          openDialogButtonText="Open add attributes dialog"
          items={mapAttributeViewsToInfoItems(
            attributes.filter(
              (attribute) => chosenAttributes.filter((chosen) => chosen.attribute.id === attribute.id).length === 0,
            ),
          )}
          onAdd={(ids) => {
            const attributesToAdd: AttributeTypeReferenceView[] = [];
            ids.forEach((id) => {
              const targetAttribute = attributeQuery.data?.find((x) => x.id === id);
              if (targetAttribute) attributesToAdd.push({ attribute: targetAttribute, minCount: 1, maxCount: null });
            });
            setAttributes([...chosenAttributes, ...attributesToAdd]);
          }}
        />
      }
    >
      <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.xl}>
        {chosenAttributes.map((attribute, index) => {
          return (
            <AttributeRow
              key={attribute.attribute.id}
              field={attribute}
              remove={() => handleRemove(index)}
              value={attribute}
              onChange={handleAttributeRowChange}
            />
          );
        })}
      </Flexbox>
    </FormSection>
  );
};

export default AttributesStep;
