import { Aspect } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { Control, useFieldArray, UseFormRegister } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { TextResources } from "../../../../assets/text";
import textResources from "../../../../assets/text/TextResources";
import { Box, Flexbox } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { useGetAttributes } from "../../../../data/queries/tyle/queriesAttribute";
import { AttributeInfoButton } from "../../../home/components/about/components/attribute/AttributeInfoButton";
import { FormNodeLib } from "../../types/formNodeLib";
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
    <Box
      as={"fieldset"}
      display={"flex"}
      flexDirection={"column"}
      justifyContent={"center"}
      gap={theme.tyle.spacing.medium}
      border={0}
      p={"0"}
    >
      <Flexbox gap={theme.tyle.spacing.medium} justifyContent={"space-between"}>
        <Text variant={"headline-medium"}>{TextResources.ATTRIBUTE_TITLE}</Text>
        <SelectAttributeDialog attributes={attributeItems} onAdd={(ids) => onAddAttributes(ids, attributeFields)} />
      </Flexbox>

      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.medium}>
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
    </Box>
  );
};
