import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { UseFormRegisterReturn } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Flexbox } from "../../../complib/layouts";
import { useGetAttributes } from "../../../data/queries/tyle/queriesAttribute";
import { UpdateEntity } from "../../../data/types/updateEntity";
import { InfoItemButton } from "../../common/infoItem";
import { ValueObject } from "../types/valueObject";
import { getSelectItemsFromAttributeLibCms, onAddValueObject } from "./FormAttributes.helpers";
import { FormSection } from "./FormSection";
import { SelectItemDialog } from "./SelectItemDialog";

export interface FormAttributesProps {
  fields: UpdateEntity<ValueObject<string>>[];
  append: (item: ValueObject<string>) => void;
  remove: (index: number) => void;
  register: (index: number) => UseFormRegisterReturn;
  prepareAttributes?: (attributes: AttributeLibCm[]) => AttributeLibCm[];
}

/**
 * Reusable form section for adding attributes to models that support them
 *
 * @param fields
 * @param append
 * @param remove
 * @param register
 * @param prepareAttributes pass a function to alter the attribute data before it is shown to the user
 * @constructor
 */
export const FormAttributes = ({ fields, append, remove, register, prepareAttributes }: FormAttributesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "attributes" });
  const attributeQuery = useGetAttributes();
  const attributes = prepareAttributes ? prepareAttributes(attributeQuery.data ?? []) : attributeQuery.data;
  const attributeItems = getSelectItemsFromAttributeLibCms(attributes);

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
          onAdd={(ids) => onAddValueObject(ids, fields, append)}
        />
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {fields.map((field, index) => {
          const attribute = attributeItems.find((x) => x.id === field.value);
          return (
            attribute && (
              <InfoItemButton
                key={field.id}
                {...register(index)}
                {...attribute}
                actionable
                actionIcon={<Trash />}
                actionText={t("remove")}
                onAction={() => remove(index)}
              />
            )
          );
        })}
      </Flexbox>
    </FormSection>
  );
};
