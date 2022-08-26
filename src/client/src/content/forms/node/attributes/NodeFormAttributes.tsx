import { Aspect } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { Control, useFieldArray, UseFormRegister } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Flexbox } from "../../../../complib/layouts";
import { useGetAttributes } from "../../../../data/queries/tyle/queriesAttribute";
import { InfoItemButton } from "../../../common/infoItem";
import { FormSection } from "../../common/FormSection";
import { SelectItemDialog } from "../../common/SelectItemDialog";
import { FormNodeLib } from "../types/formNodeLib";
import { getSelectItemsFromAttributeLibCms, onAddAttributes, prepareAttributes } from "./NodeFormAttributes.helpers";

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
  const attributeItems = getSelectItemsFromAttributeLibCms(filteredAttributes);

  return (
    <FormSection
      title={t("title")}
      action={
        <SelectItemDialog
          title={t("dialog.title")}
          description={t("dialog.description")}
          searchFieldText={t("dialog.search")}
          addItemsButtonText={t("dialog.add")}
          openDialogButtonText={t("open")}
          items={attributeItems}
          onAdd={(ids) => onAddAttributes(ids, attributeFields)}
        />
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {attributeFields.fields.map((field, index) => {
          const attribute = attributeItems.find((x) => x.id === field.value);
          return (
            attribute && (
              <InfoItemButton
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
    </FormSection>
  );
};
