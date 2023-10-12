import { XCircle } from "@styled-icons/heroicons-outline";
import { Counter, Flexbox, Input, Token } from "@mimirorg/component-library";
import { useGetAttributes } from "external/sources/attribute/attribute.queries";
import { onAddAttributes, resolveSelectedAndAvailableAttributes } from "features/entities/common/form-attributes/FormAttributes.helpers";
import { FormSection } from "features/entities/common/form-section/FormSection";
import { SelectItemDialog } from "features/entities/common/select-item-dialog/SelectItemDialog";
import { Control, Controller, UseFormRegisterReturn } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { AttributeTypeReferenceRequest } from "common/types/common/attributeTypeReferenceRequest";
import { AttributeView } from "common/types/attributes/attributeView";
import { TerminalFormFields } from "features/entities/terminal/TerminalForm.helpers";

export interface FormAttributesProps {
  control: Control<TerminalFormFields>
  fields: AttributeTypeReferenceRequest[];
  append: (item: AttributeTypeReferenceRequest) => void;
  remove: (index: number) => void;
  register: (index: number) => UseFormRegisterReturn;
  preprocess?: (attributes?: AttributeView[]) => AttributeView[];
  canAddAttributes?: boolean;
  canRemoveAttributes?: boolean;
  canAddAttributeGroups?: boolean;
  canRemoveAttributeGroups?: boolean;
  limitedAttributes?: AttributeView[];
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
 * @param limitedAttributes attributes that cannot be removed, even if removing attributes is allowed
 * @constructor
 */
export const FormAttributes = ({
  control,
  fields,
  append,
  remove,
  register,
  preprocess,
  canAddAttributes = true,
  canRemoveAttributes = true,
  limitedAttributes = [],
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
      <Flexbox flexWrap={"wrap"} gap={theme.mimirorg.spacing.xl}>
        {fields.map((field, index) => {
          const attribute = selected.find((x) => x.id === field.attributeId);
          return (
            attribute && <><Token
              variant={"secondary"}
              key={attribute.id}
              {...register(index)}
              actionable={canRemoveAttributes && !limitedAttributes.map((x) => x.id).includes(attribute.id ?? "")}
              actionIcon={<XCircle />}
              actionText={t("common.attributes.remove")}
              onAction={() => remove(index)}
              dangerousAction
            >
              {attribute.name}
            </Token>
            <Controller
              control={control}
              name={`attributes.${index}.minCount`}
              render={({ field: { value, ...rest }}) => (
                <Counter
                  {...rest}
                  min={0}
                  value={value}
                />
              )}
            />
            <Controller
              control={control}
              name={`attributes.${index}.maxCount`}
              render={({ field: { value, ...rest }}) => (
                <Counter
                  {...rest}
                  min={1}
                  value={value ?? 0}
                />
              )}
            />
            </>
          );
        })}
      </Flexbox>
    </FormSection>
  );
};
