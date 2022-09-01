import { Control, Controller, useFieldArray } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormField } from "../../../../complib/form";
import { Select } from "../../../../complib/inputs";
import { Flexbox } from "../../../../complib/layouts";
import { useGetAttributesReference } from "../../../../data/queries/tyle/queriesAttribute";
import { FieldsCard } from "../../common/FieldsCard";
import { FormAddButton } from "../../common/FormAddButton";
import { FormSection } from "../../common/FormSection";
import { FormAttributeLib } from "../types/formAttributeLib";

export interface AttributeFormValuesProps {
  control: Control<FormAttributeLib>;
}

export const AttributeFormReferences = ({ control }: AttributeFormValuesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation");
  const referenceQuery = useGetAttributesReference();
  const referenceFields = useFieldArray({ control, name: "typeReferences" });
  const onRemove = (index: number) => referenceFields.remove(index);

  return (
    <FormSection
      title={t("references.title")}
      action={
        <FormAddButton
          buttonText={t("references.add")}
          onClick={() => referenceFields.append({ name: "", iri: "", source: "", subIri: "", subName: "" })}
        />
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {referenceFields.fields.map((field, index) => (
          <FieldsCard
            key={field.id}
            index={index + 1}
            removeText={t("references.remove")}
            onRemove={() => onRemove(index)}
          >
            <Controller
              control={control}
              name={`typeReferences.${index}`}
              render={({ field: { value, ref, ...rest } }) => (
                <FormField label={t("references.reference")}>
                  <Select
                    {...rest}
                    selectRef={ref}
                    placeholder={t("common.templates.select", { object: t("references.reference").toLowerCase() })}
                    options={referenceQuery.data}
                    isLoading={referenceQuery.isLoading}
                    getOptionLabel={(x) => x.name}
                    getOptionValue={(x) => x.iri}
                    value={referenceQuery.data?.find((x) => x.iri == value.iri)}
                  />
                </FormField>
              )}
            />
          </FieldsCard>
        ))}
      </Flexbox>
    </FormSection>
  );
};
