import { Control, Controller, useFieldArray } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormField } from "../../../../complib/form";
import { Input } from "../../../../complib/inputs";
import { Flexbox } from "../../../../complib/layouts";
import { FieldsCard } from "../../common/FieldsCard";
import { FormAddButton } from "../../common/FormAddButton";
import { FormSection } from "../../common/FormSection";
import { FormAttributeLib } from "../types/formAttributeLib";

export interface AttributeFormValuesProps {
  control: Control<FormAttributeLib>;
}

export const AttributeFormValues = ({ control }: AttributeFormValuesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "values" });
  const valueFields = useFieldArray({ control, name: "selectValues" });
  const onRemove = (index: number) => valueFields.remove(index);

  return (
    <FormSection
      title={t("title")}
      action={<FormAddButton buttonText={t("add")} onClick={() => valueFields.append({ value: "" })} />}
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {valueFields.fields.map((field, index) => (
          <FieldsCard key={field.id} index={index + 1} removeText={t("remove")} onRemove={() => onRemove(index)}>
            <Controller
              control={control}
              name={`selectValues.${index}`}
              render={({ field: { value, ...rest } }) => (
                <FormField label={t("value")}>
                  <Input {...rest} id={field.id} value={value.value} placeholder={t("placeholders.value")} />
                </FormField>
              )}
            />
          </FieldsCard>
        ))}
      </Flexbox>
    </FormSection>
  );
};
