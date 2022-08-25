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

export const AttributeFormReferences = ({ control }: AttributeFormValuesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "references" });
  const referenceFields = useFieldArray({ control, name: "typeReferences" });
  const onRemove = (index: number) => referenceFields.remove(index);

  return (
    <FormSection
      title={t("title")}
      action={<FormAddButton buttonText={t("add")} onClick={() => referenceFields.append({ name: "", iri: "" })} />}
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {referenceFields.fields.map((field, index) => (
          <FieldsCard key={field.id} index={index + 1} removeText={t("remove")} onRemove={() => onRemove(index)}>
            <Controller
              control={control}
              name={`typeReferences.${index}.name`}
              render={({ field: { value, ...rest } }) => (
                <FormField label={t("name")}>
                  <Input {...rest} id={field.id} value={value} placeholder={t("placeholders.name")} />
                </FormField>
              )}
            />
            <Controller
              control={control}
              name={`typeReferences.${index}.iri`}
              render={({ field: { value, ...rest } }) => (
                <FormField label={t("iri")}>
                  <Input {...rest} id={field.id} value={value} placeholder={t("placeholders.iri")} />
                </FormField>
              )}
            />
          </FieldsCard>
        ))}
      </Flexbox>
    </FormSection>
  );
};
