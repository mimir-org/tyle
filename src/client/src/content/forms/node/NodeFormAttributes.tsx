import { Control, useFieldArray } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { TextResources } from "../../../assets/text";
import { Box, Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { useGetAttributes } from "../../../data/queries/tyle/queriesAttribute";
import { AttributeInfoButton } from "../../home/components/about/components/attribute/AttributeInfoButton";
import { FormNodeLib } from "../types/formNodeLib";
import { SelectAttributeDialog } from "./SelectAttributeDialog";
import { Aspect } from "../../../models/tyle/enums/aspect";
import { getAttributeItems, onAddAttributes, prepareAttributes } from "./NodeFormAttributes.helpers";

export interface NodeFormAttributesProps {
  control: Control<FormNodeLib>;
  aspects?: Aspect[];
}

export const NodeFormAttributes = ({ control, aspects }: NodeFormAttributesProps) => {
  const theme = useTheme();
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const attributeQuery = useGetAttributes();
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
        {attributeFields.fields.map((field) => {
          const attribute = attributeItems.find((x) => x.id === field.value);
          return attribute && <AttributeInfoButton key={field.id} {...attribute} />;
        })}
      </Flexbox>
    </Box>
  );
};
