import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { InfoItemButton } from "common/components/info-item";
import { UpdateEntity } from "common/types/updateEntity";
import { Flexbox } from "complib/layouts";
import { useGetAttributes } from "external/sources/attribute/attribute.queries";
import {
  onAddAttributes,
  resolveSelectedAndAvailableAttributes,
} from "features/entities/common/form-attributes/FormAttributes.helpers";
import { FormSection } from "features/entities/common/form-section/FormSection";
import { SelectItemDialog } from "features/entities/common/select-item-dialog/SelectItemDialog";
import { ValueObject } from "features/entities/types/valueObject";
import { UseFormRegisterReturn } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";

export interface FormAttributesProps {
  fields: ValueObject<UpdateEntity<AttributeLibAm>>[];
  append: (item: ValueObject<UpdateEntity<AttributeLibAm>>) => void;
  remove: (index: number) => void;
  register: (index: number) => UseFormRegisterReturn;
  preprocess?: (attributes?: AttributeLibCm[]) => AttributeLibCm[];
}

/**
 * Reusable form section for adding attributes to models that support them
 *
 * @param fields
 * @param append
 * @param remove
 * @param register
 * @param preprocess pass a function to alter the attribute data before it is shown to the user
 * @constructor
 */
export const FormAttributes = ({ fields, append, remove, register, preprocess }: FormAttributesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "attributes" });

  const attributeQuery = useGetAttributes();
  const attributes = preprocess ? preprocess(attributeQuery.data) : attributeQuery.data ?? [];
  const [available, selected] = resolveSelectedAndAvailableAttributes(fields, attributes);

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
          items={available}
          onAdd={(ids) => onAddAttributes(ids, attributes, append)}
        />
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {fields.map((field, index) => {
          const attribute = selected.find((x) => x.id === field.value.id);
          return (
            attribute && (
              <InfoItemButton
                key={field.value.id}
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
