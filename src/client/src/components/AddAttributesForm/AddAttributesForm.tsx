import { useGetAttributes } from "api/attribute.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { mapAttributeViewsToInfoItems } from "helpers/mappers.helpers";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";
import { prepareAttributes } from "./AddAttributesForm.helpers";
import { AttributeRowsWrapper } from "./AddAttributesForm.styled";
import AttributeRow from "./AttributeRow";

interface AttributesStepProps {
  attributes: AttributeTypeReferenceView[];
  setAttributes: (attributes: AttributeTypeReferenceView[]) => void;
}

const AddAttributesForm = ({ attributes, setAttributes }: AttributesStepProps) => {
  const attributeQuery = useGetAttributes();
  const attributeInfoItems = mapAttributeViewsToInfoItems(prepareAttributes(attributeQuery.data) ?? []);

  const availableAttributes = attributeInfoItems.filter(
    (attribute) =>
      attributes.filter((selectedAttribute) => selectedAttribute.attribute.id === attribute.id).length === 0,
  );

  const handleAttributeRowChange = (changedAttribute: AttributeTypeReferenceView) => {
    const nextAttributes = [...attributes];

    const changedAttributeIndex = nextAttributes.findIndex(
      (attribute) => attribute.attribute.id === changedAttribute.attribute.id,
    );
    if (changedAttributeIndex >= 0) {
      nextAttributes[changedAttributeIndex] = changedAttribute;
    }

    setAttributes(nextAttributes);
  };

  const handleAdd = (addedIds: string[]) => {
    const attributesToAdd: AttributeTypeReferenceView[] = [];
    addedIds.forEach((id) => {
      const targetAttribute = attributeQuery.data?.find((x) => x.id === id);
      if (targetAttribute) attributesToAdd.push({ attribute: targetAttribute, minCount: 1, maxCount: null });
    });
    setAttributes([...attributes, ...attributesToAdd]);
  };

  const handleRemove = (removedAttribute: AttributeTypeReferenceView) => {
    setAttributes(attributes.filter((attribute) => attribute.attribute.id !== removedAttribute.attribute.id));
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
          items={availableAttributes}
          onAdd={handleAdd}
        />
      }
    >
      <AttributeRowsWrapper>
        {attributes.map((attribute) => {
          return (
            <AttributeRow
              key={attribute.attribute.id}
              remove={() => handleRemove(attribute)}
              value={attribute}
              onChange={handleAttributeRowChange}
            />
          );
        })}
      </AttributeRowsWrapper>
    </FormSection>
  );
};

export default AddAttributesForm;
