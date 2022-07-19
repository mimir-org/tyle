import { Aspect } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { Control, useFieldArray, UseFormRegister } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Flexbox } from "../../../../complib/layouts";
import { useGetAttributes } from "../../../../data/queries/tyle/queriesAttribute";
import { AttributeInfoButton } from "../../../common/attribute";
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
  const { t } = useTranslation("translation", { keyPrefix: "attributes" });
  const attributeQuery = useGetAttributes();
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });
  const filteredAttributes = prepareAttributes(attributeQuery.data, aspects);
  const attributeItems = getAttributeItems(filteredAttributes);

  return (
    <NodeFormSection
      title={t("title")}
      action={
        <SelectAttributeDialog attributes={attributeItems} onAdd={(ids) => onAddAttributes(ids, attributeFields)} />
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {attributeFields.fields.map((field, index) => {
          const attribute = attributeItems.find((x) => x.id === field.value);
          return (
            attribute && (
              <AttributeInfoButton
                key={field.id}
                {...register(`attributeIdList.${index}`)}
                {...attribute}
                actionable
                actionIcon={<Trash />}
                actionText={t("remove")}
                onAction={() => attributeFields.remove(index)}
              />
            )
          );
        })}
      </Flexbox>
    </NodeFormSection>
  );
};
