import { TypeReferenceAm, TypeReferenceCm } from "@mimirorg/typelibrary-types";
import { Control, Controller, useFieldArray } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormField } from "../../../../complib/form";
import { Select } from "../../../../complib/inputs";
import { Flexbox } from "../../../../complib/layouts";
import { FieldsCard } from "../fields-card/FieldsCard";
import { FormAddButton } from "../form-add-button/FormAddButton";
import { FormSection } from "../form-section/FormSection";

export type HasReferences = { typeReferences?: TypeReferenceAm[] };

export interface FormReferencesProps {
  control: Control<HasReferences>;
  references: TypeReferenceCm[];
  isLoading: boolean;
}

/**
 * Reusable form section for adding references to models that support them
 *
 * @param control excepts a Control<T> where T has a typeReference property
 * @param references list of references which the user can pick from
 * @param isLoading fetch status of reference data
 * @constructor
 *
 * @example Controller type workaround by as casting
 * <FormReferences
 *   control={control as unknown as Control<HasReferences>}
 *   references={referenceQuery.data ?? []}
 *   isLoading={referenceQuery.isLoading}
 * />
 */
export const FormReferences = ({ control, references, isLoading }: FormReferencesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation");

  const referenceFields = useFieldArray({ control: control, name: "typeReferences" });
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
