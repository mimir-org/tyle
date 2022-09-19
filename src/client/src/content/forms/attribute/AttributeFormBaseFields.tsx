import {
  Aspect,
  Discipline,
  MimirorgPermission,
  QuantityDatumType,
  Select as AttributeSelect,
} from "@mimirorg/typelibrary-types";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Button } from "../../../complib/buttons";
import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Flexbox } from "../../../complib/layouts";
import { useGetQuantityDatum } from "../../../data/queries/tyle/queriesAttribute";
import { useGetFilteredCompanies } from "../../../hooks/useGetFilteredCompanies";
import { getValueLabelObjectsFromEnum } from "../../../utils/getValueLabelObjectsFromEnum";
import { PlainLink } from "../../utils/PlainLink";
import { onChangeSelectType } from "./AttributeFormBaseFields.helpers";
import { AttributeFormBaseFieldsContainer } from "./AttributeFormBaseFields.styled";
import { AttributeFormPreview } from "./AttributeFormPreview";
import { FormAttributeLib } from "./types/formAttributeLib";

interface AttributeFormBaseFieldsProps {
  isPrefilled?: boolean;
}

/**
 * Component which contains all shared fields for variations of the attribute form.
 *
 * @param isPrefilled
 * @constructor
 */
export const AttributeFormBaseFields = ({ isPrefilled }: AttributeFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { control, register, resetField, formState } = useFormContext<FormAttributeLib>();
  const { errors } = formState;

  const specifiedScopeQuery = useGetQuantityDatum(QuantityDatumType.QuantityDatumSpecifiedScope);
  const rangeSpecifyingQuery = useGetQuantityDatum(QuantityDatumType.QuantityDatumRangeSpecifying);
  const regularitySpecifiedQuery = useGetQuantityDatum(QuantityDatumType.QuantityDatumRegularitySpecified);
  const specifiedProvenanceQuery = useGetQuantityDatum(QuantityDatumType.QuantityDatumSpecifiedProvenance);
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);
  const disciplineOptions = getValueLabelObjectsFromEnum<Discipline>(Discipline);
  const selectOptions = getValueLabelObjectsFromEnum<AttributeSelect>(AttributeSelect);
  const companies = useGetFilteredCompanies(MimirorgPermission.Write);

  return (
    <AttributeFormBaseFieldsContainer>
      <AttributeFormPreview control={control} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("attribute.name")} error={errors.name}>
          <Input placeholder={t("attribute.placeholders.name")} {...register("name")} />
        </FormField>

        <Controller
          control={control}
          name={"aspect"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.aspect")}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("attribute.aspect").toLowerCase() })}
                options={aspectOptions}
                getOptionLabel={(x) => x.label}
                onChange={(x) => onChange(x?.value)}
                value={aspectOptions.find((x) => x.value === value)}
                isDisabled={isPrefilled}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"discipline"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.discipline")} error={errors.discipline}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("attribute.discipline").toLowerCase() })}
                options={disciplineOptions}
                getOptionLabel={(x) => x.label}
                onChange={(x) => onChange(x?.value)}
                value={disciplineOptions.find((x) => x.value === value)}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"companyId"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.owner")} error={errors.companyId}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("node.owner").toLowerCase() })}
                options={companies}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange(x?.id);
                }}
                value={companies.find((x) => x.id === value)}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"select"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.select")} error={errors.select}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", {
                  object: t("attribute.placeholders.select").toLowerCase(),
                })}
                options={selectOptions}
                getOptionLabel={(x) => x.label}
                onChange={(x) => {
                  onChangeSelectType(resetField, x?.value);
                  onChange(x?.value);
                }}
                value={selectOptions.find((x) => x.value === value)}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"quantityDatumSpecifiedScope"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.quantityDatumSpecifiedScope")} error={errors.quantityDatumSpecifiedScope}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", {
                  object: t("attribute.quantityDatumSpecifiedScope").toLowerCase(),
                })}
                options={specifiedScopeQuery.data}
                isLoading={specifiedScopeQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={specifiedScopeQuery.data?.find((x) => x.name === value)}
                isClearable
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"quantityDatumSpecifiedProvenance"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField
              label={t("attribute.quantityDatumSpecifiedProvenance")}
              error={errors.quantityDatumSpecifiedProvenance}
            >
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", {
                  object: t("attribute.quantityDatumSpecifiedProvenance").toLowerCase(),
                })}
                options={specifiedProvenanceQuery.data}
                isLoading={specifiedProvenanceQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={specifiedProvenanceQuery.data?.find((x) => x.name === value)}
                isClearable
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"quantityDatumRangeSpecifying"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.quantityDatumRangeSpecifying")} error={errors.quantityDatumRangeSpecifying}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", {
                  object: t("attribute.quantityDatumRangeSpecifying").toLowerCase(),
                })}
                options={rangeSpecifyingQuery.data}
                isLoading={rangeSpecifyingQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={rangeSpecifyingQuery.data?.find((x) => x.name === value)}
                isClearable
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"quantityDatumRegularitySpecified"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField
              label={t("attribute.quantityDatumRegularitySpecified")}
              error={errors.quantityDatumRegularitySpecified}
            >
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", {
                  object: t("attribute.quantityDatumRegularitySpecified").toLowerCase(),
                })}
                options={regularitySpecifiedQuery.data}
                isLoading={regularitySpecifiedQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={regularitySpecifiedQuery.data?.find((x) => x.name === value)}
                isClearable
              />
            </FormField>
          )}
        />

        <FormField label={t("attribute.description")} error={errors.description}>
          <Textarea placeholder={t("attribute.placeholders.description")} {...register("description")} />
        </FormField>
      </Flexbox>

      <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{t("common.submit")}</Button>
      </Flexbox>
    </AttributeFormBaseFieldsContainer>
  );
};
