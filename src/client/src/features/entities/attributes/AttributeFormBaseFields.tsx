import { FormBaseFieldsContainer, FormField, Input, Select, Textarea, Token } from "@mimirorg/component-library";
import { Controller, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { ProvenanceQualifier } from "common/types/attributes/provenanceQualifier";
import { AttributeFormFields, UnitRequirements, predicateInfoItem } from "./AttributeForm.helpers";
import { RangeQualifier } from "common/types/attributes/rangeQualifier";
import { RegularityQualifier } from "common/types/attributes/regularityQualifier";
import { ScopeQualifier } from "common/types/attributes/scopeQualifier";
import { FormSection } from "../common/form-section/FormSection";
import { SelectItemDialog } from "../common/select-item-dialog/SelectItemDialog";
import { useGetPredicates } from "external/sources/predicate/predicate.queries";
import { XCircle } from "@styled-icons/heroicons-outline";

interface AttributeFormBaseFieldsProps {
  limited?: boolean;
}

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */

export const AttributeFormBaseFields = ({ limited }: AttributeFormBaseFieldsProps) => {
  const { t } = useTranslation("entities");
  const {
    control,
    register,
    setValue,
    formState: { errors },
  } = useFormContext<AttributeFormFields>();

  const predicateQuery = useGetPredicates();
  const predicateInfoItems = predicateQuery.data?.map((p) => predicateInfoItem(p)) ?? [];
  const chosenPredicate = useWatch({ control, name: "predicate" });

  const unitRequirementsOptions = getOptionsFromEnum<UnitRequirements>(UnitRequirements);
  const provenanceQualifierOptions = getOptionsFromEnum<ProvenanceQualifier>(ProvenanceQualifier);
  const rangeQualifierOptions = getOptionsFromEnum<RangeQualifier>(RangeQualifier);
  const regularityQualifierOptions = getOptionsFromEnum<RegularityQualifier>(RegularityQualifier);
  const scopeQualifierOptions = getOptionsFromEnum<ScopeQualifier>(ScopeQualifier);

  return (
    <FormBaseFieldsContainer>
      <FormField label={t("attribute.name")} error={errors.name}>
        <Input placeholder={t("attribute.placeholders.name")} {...register("name")} disabled={limited} />
      </FormField>

      <FormField label={t("attribute.description")} error={errors.description}>
        <Textarea placeholder={t("attribute.placeholders.description")} {...register("description")} />
      </FormField>

      <FormSection
        title={t("attribute.predicate.title")}
        action={
          !chosenPredicate && (
            <SelectItemDialog
              title={t("attribute.dialog.title")}
              description={t("attribute.dialog.description")}
              searchFieldText={t("attribute.dialog.search")}
              addItemsButtonText={t("attribute.dialog.add")}
              openDialogButtonText={t("attribute.dialog.open")}
              items={predicateInfoItems}
              onAdd={(ids) => {
                setValue("predicate", predicateQuery.data?.find((x) => x.id ===Number(ids[0])));
              }}
              isMultiSelect={false}
            />
          )
        }
      >
        <Input {...register("predicate")} type="hidden" />
        {chosenPredicate && (
          <Token
            variant={"secondary"}
            actionable
            actionIcon={<XCircle />}
            actionText={t("attribute.predicate.remove")}
            onAction={() => setValue("predicate", undefined)}
            dangerousAction
          >
            {predicateInfoItems.find((x) => x.id === chosenPredicate.toString())?.name}
          </Token>
        )}
      </FormSection>

      <FormField label={t("attribute.unitRequirement")} error={errors.unitRequirement}>
        <Controller
          control={control}
          name={"unitRequirement"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("attribute.unitRequirement").toLowerCase() })}
              options={unitRequirementsOptions}
              getOptionLabel={(x) => x.label}
              onChange={(x) => {
                onChange(x?.value);
              }}
              value={unitRequirementsOptions.find((x) => x.value === value)}
            />
          )}
        />
      </FormField>

      <FormField label={t("attribute.provenanceQualifier")} error={errors.provenanceQualifier}>
        <Controller
          control={control}
          name={"provenanceQualifier"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("attribute.provenanceQualifier").toLowerCase() })}
              options={provenanceQualifierOptions}
              getOptionLabel={(x) => x.label}
              onChange={(x) => {
                onChange(x?.value);
              }}
              value={provenanceQualifierOptions.find((x) => x.value === value)}
              isClearable
            />
          )}
        />
      </FormField>

      <FormField label={t("attribute.rangeQualifier")} error={errors.rangeQualifier}>
        <Controller
          control={control}
          name={"rangeQualifier"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("attribute.rangeQualifier").toLowerCase() })}
              options={rangeQualifierOptions}
              getOptionLabel={(x) => x.label}
              onChange={(x) => {
                onChange(x?.value);
              }}
              value={rangeQualifierOptions.find((x) => x.value === value)}
              isClearable
            />
          )}
        />
      </FormField>

      <FormField label={t("attribute.regularityQualifier")} error={errors.regularityQualifier}>
        <Controller
          control={control}
          name={"regularityQualifier"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("attribute.regularityQualifier").toLowerCase() })}
              options={regularityQualifierOptions}
              getOptionLabel={(x) => x.label}
              onChange={(x) => {
                onChange(x?.value);
              }}
              value={regularityQualifierOptions.find((x) => x.value === value)}
              isClearable
            />
          )}
        />
      </FormField>

      <FormField label={t("attribute.scopeQualifier")} error={errors.scopeQualifier}>
        <Controller
          control={control}
          name={"scopeQualifier"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("attribute.scopeQualifier").toLowerCase() })}
              options={scopeQualifierOptions}
              getOptionLabel={(x) => x.label}
              onChange={(x) => {
                onChange(x?.value);
              }}
              value={scopeQualifierOptions.find((x) => x.value === value)}
              isClearable
            />
          )}
        />
      </FormField>
    </FormBaseFieldsContainer>
  );
};
