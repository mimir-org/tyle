import { TypeReferenceAm, TypeReferenceCm } from "@mimirorg/typelibrary-types";
import { FormField } from "complib/form";
import { Select } from "complib/inputs";
import { Flexbox } from "complib/layouts";
import { FieldsCard } from "features/entities/common/fields-card/FieldsCard";
import { FormAddButton } from "features/entities/common/form-add-button/FormAddButton";
import { FormSection } from "features/entities/common/form-section/FormSection";
import { Controller, useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export type HasReferences = { typeReferences?: TypeReferenceAm[] };

export interface FormReferencesProps {
  references: TypeReferenceCm[];
  isLoading: boolean;
}

/**
 * Reusable form section for adding references to models that support them
 * Expects to be used in a context of useForm<T> where T has a typeReference property
 *
 * @param references list of references which the user can pick from
 * @param isLoading fetch status of reference data
 * @constructor
 */
export const FormReferences = ({ references, isLoading }: FormReferencesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, formState } = useFormContext<HasReferences>();
  const { errors } = formState;

  const referenceFields = useFieldArray({ control: control, name: "typeReferences" });
  const onRemove = (index: number) => referenceFields.remove(index);

  return (
    <FormSection
      title={t("common.references.title")}
      action={
        <FormAddButton
          buttonText={t("common.references.add")}
          onClick={() => referenceFields.append({ name: "", iri: "", source: "" })}
        />
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {referenceFields.fields.map((field, index) => (
          <FieldsCard
            key={`${index},${field.id}`}
            index={index + 1}
            removeText={t("common.references.remove")}
            onRemove={() => onRemove(index)}
          >
            <Controller
              control={control}
              name={`typeReferences.${index}`}
              render={({ field: { value, ref, ...rest } }) => (
                <FormField label={t("common.references.reference")} error={errors.typeReferences?.[index]?.name}>
                  <Select
                    {...rest}
                    selectRef={ref}
                    placeholder={t("common.templates.select", {
                      object: t("common.reference.reference").toLowerCase(),
                    })}
                    options={references}
                    isLoading={isLoading}
                    getOptionLabel={(x) => x.name}
                    getOptionValue={(x) => x.iri}
                    value={references.find((x) => x.iri == value.iri)}
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
