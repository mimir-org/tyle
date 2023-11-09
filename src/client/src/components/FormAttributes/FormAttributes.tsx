import { Flexbox } from "@mimirorg/component-library";
import { useGetAttributes } from "api/attribute.queries";
import { BlockFormFields } from "components/BlockForm/BlockForm.helpers";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { TerminalFormFields } from "components/TerminalForm/TerminalForm.helpers";
import { Controller, useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import AttributeRow from "./AttributeRow";
import { onAddAttributes, resolveSelectedAndAvailableAttributes } from "./FormAttributes.helpers";
import { prepareAttributes } from "./prepareAttributes";

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
const FormAttributes = () => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  type BlockOrTerminalFormFields = BlockFormFields | TerminalFormFields;

  const { control } = useFormContext<BlockOrTerminalFormFields>();

  const attributeFields = useFieldArray({ control, name: "attributes" });
  const attributeQuery = useGetAttributes();
  const attributes = prepareAttributes(attributeQuery.data) ?? [];
  const [available, _] = resolveSelectedAndAvailableAttributes(attributeFields.fields, attributes);

  return (
    <FormSection
      title={t("common.attributes.title")}
      action={
        <SelectItemDialog
          title={t("common.attributes.dialog.title")}
          description={t("common.attributes.dialog.description")}
          searchFieldText={t("common.attributes.dialog.search")}
          addItemsButtonText={t("common.attributes.dialog.add")}
          openDialogButtonText={t("common.attributes.open")}
          items={available}
          onAdd={(ids) => onAddAttributes(ids, attributes, attributeFields.append)}
        />
      }
    >
      <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.xl}>
        {attributeFields.fields.map((field, index) => {
          return (
            <Controller
              key={field.id}
              control={control}
              name={`attributes.${index}`}
              render={({ field: { value, onChange } }) => (
                <AttributeRow
                  field={field}
                  remove={() => attributeFields.remove(index)}
                  value={value}
                  onChange={onChange}
                />
              )}
            />
          );
        })}
      </Flexbox>
    </FormSection>
  );
};

export default FormAttributes;
