import { Control, Controller, useFieldArray } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormField } from "../../../../complib/form";
import { Input } from "../../../../complib/inputs";
import { Flexbox } from "../../../../complib/layouts";
import { FieldsCard } from "../../common/fields-card/FieldsCard";
import { FormAddButton } from "../../common/form-add-button/FormAddButton";
import { FormSection } from "../../common/form-section/FormSection";
import { FormAttributeLib } from "../types/formAttributeLib";

export interface AttributeFormValuesProps {
  control: Control<FormAttributeLib>;
}

export const AttributeFormValues = ({ control }: AttributeFormValuesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "values" });
  const selectValueFields = useFieldArray({ control, name: "selectValues" });
  const onRemove = (index: number) => selectValueFields.remove(index);

  return (
    <FormSection
      title={t("title")}
      action={<FormAddButton buttonText={t("add")} onClick={() => selectValueFields.append({ value: "" })} />}
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {selectValueFields.fields.map((selectValueField, index) => (
          <FieldsCard
            key={selectValueField.id}
            index={index + 1}
            removeText={t("remove")}
            onRemove={() => onRemove(index)}
          >
            <Controller
              control={control}
              name={`selectValues.${index}.value`}
              render={({ field }) => (
                <FormField label={t("value")}>
                  <Input {...field} id={selectValueField.id} placeholder={t("placeholders.value")} width={"250px"} />
                </FormField>
              )}
            />
          </FieldsCard>
        ))}
      </Flexbox>
    </FormSection>
  );
};
