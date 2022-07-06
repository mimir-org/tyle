import { Aspect } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { Control, useFieldArray, UseFormRegister } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { TextResources } from "../../../../assets/text";
import textResources from "../../../../assets/text/TextResources";
import { Flexbox } from "../../../../complib/layouts";
import { useGetAttributes } from "../../../../data/queries/tyle/queriesAttribute";
import { AttributeInfoButton } from "../../../home/components/about/components/attribute/AttributeInfoButton";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormSection } from "../NodeFormSection";
import { getAttributeItems, onAddAttributes, prepareAttributes } from "./NodeFormAttributes.helpers";
import { SelectAttributeDialog } from "./SelectAttributeDialog";

export interface NodeFormAttributesProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
  aspects?: Aspect[];
}

export const NodeFormAttributes = ({ control, aspects, register }: NodeFormAttributesProps) => {
  const theme = useTheme();
  const attributeQuery = useGetAttributes();
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });
  const filteredAttributes = prepareAttributes(attributeQuery.data, aspects);
  const attributeItems = getAttributeItems(filteredAttributes);

  return (
    <NodeFormSection
      title={TextResources.ATTRIBUTE_TITLE}
      action={
        <SelectAttributeDialog attributes={attributeItems} onAdd={(ids) => onAddAttributes(ids, attributeFields)} />
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {attributeFields.fields.map((field, index) => {
          const attribute = attributeItems.find((x) => x.id === field.value);
          const { ref, ...registerRest } = register(`attributeIdList.${index}`);
          return (
            attribute && (
              <AttributeInfoButton
                key={field.id}
                {...registerRest}
                {...attribute}
                actionable
                actionIcon={<Trash />}
                actionText={textResources.ATTRIBUTE_REMOVE}
                onAction={() => attributeFields.remove(index)}
                buttonRef={ref}
              />
            )
          );
        })}
      </Flexbox>
    </NodeFormSection>
  );
};
