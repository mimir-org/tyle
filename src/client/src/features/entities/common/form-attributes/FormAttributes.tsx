import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { Trash } from "@styled-icons/heroicons-outline";
import { UpdateEntity } from "common/types/updateEntity";
import { Flexbox } from "complib/layouts";
import { useGetAttributes } from "external/sources/attribute/attribute.queries";
import { InfoItemButton } from "features/common/info-item";
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
  canAddAttributes?: boolean;
  canRemoveAttributes?: boolean;
}

/**
 * Reusable form section for adding attributes to models that support them
 *
 * @param fields
 * @param append
 * @param remove
 * @param register
 * @param preprocess pass a function to alter the attribute data before it is shown to the user
 * @param canAddAttributes controls if the add action is shown
 * @param canRemoveAttributes controls if the remove action is shown
 * @constructor
 */
export const FormAttributes = ({
  fields,
  append,
  remove,
  register,
  preprocess,
  canAddAttributes = true,
  canRemoveAttributes = true,
}: FormAttributesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const attributeQuery = useGetAttributes();
  const attributes = preprocess ? preprocess(attributeQuery.data) : attributeQuery.data ?? [];
  const [available, selected] = resolveSelectedAndAvailableAttributes(fields, attributes);

  return (
    <FormSection
      title={t("common.attributes.title")}
      action={
        canAddAttributes && (
          <SelectItemDialog
            title={t("common.attributes.dialog.title")}
            description={t("common.attributes.dialog.description")}
            searchFieldText={t("common.attributes.dialog.search")}
            addItemsButtonText={t("common.attributes.dialog.add")}
            openDialogButtonText={t("common.attributes.open")}
            items={available}
            onAdd={(ids) => onAddAttributes(ids, attributes, append)}
          />
        )
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
                actionable={canRemoveAttributes}
                actionIcon={<Trash />}
                actionText={t("common.attributes.remove")}
                onAction={() => remove(index)}
              />
            )
          );
        })}
      </Flexbox>
    </FormSection>
  );
};
