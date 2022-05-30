import { Control, useFieldArray } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import { TextResources } from "../../../assets/text";
import { Box, Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { useGetAttributes } from "../../../data/queries/tyle/queriesAttribute";
import { mapAttributeLibCmToAttributeItem } from "../../../utils/mappers";
import { AttributeInfoButton } from "../../home/components/about/components/attribute/AttributeInfoButton";
import { FormNodeLib } from "../types/formNodeLib";
import { SelectAttributeDialog } from "./SelectAttributeDialog";

export interface NodeFormAttributesProps {
  control: Control<FormNodeLib>;
}

export const NodeFormAttributes = ({ control }: NodeFormAttributesProps) => {
  const theme = useTheme();
  const attributeQuery = useGetAttributes();
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });
  const attributeItems = attributeQuery.data ? attributeQuery.data.map((x) => mapAttributeLibCmToAttributeItem(x)) : [];

  const onAddAttributes = (ids: string[]) => {
    ids.forEach((id) => {
      const attributeHasNotBeenAdded = !attributeFields.fields.some((f) => f.value === id);
      if (attributeHasNotBeenAdded) {
        attributeFields.append({ value: id });
      }
    });
  };

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
        <SelectAttributeDialog attributes={attributeItems} onAdd={onAddAttributes} />
      </Flexbox>

      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.medium}>
        {attributeFields.fields.map((field) => {
          const attribute = attributeQuery.data?.find((x) => x.id === field.value);
          return (
            attribute && (
              <AttributeInfoButton
                key={field.id}
                {...mapAttributeLibCmToAttributeItem(attribute)}
              ></AttributeInfoButton>
            )
          );
        })}
      </Flexbox>
    </Box>
  );
};
