import { Aspect, Discipline, MimirorgPermission, Select as AttributeSelect } from "@mimirorg/typelibrary-types";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Button } from "../../../complib/buttons";
import { FormField } from "../../../complib/form";
import { Input, Select } from "../../../complib/inputs";
import { Flexbox } from "../../../complib/layouts";
import {
  useGetAttributesCondition,
  useGetAttributesFormat,
  useGetAttributesQualifier,
  useGetAttributesSource,
} from "../../../data/queries/tyle/queriesAttribute";
import { getValueLabelObjectsFromEnum } from "../../../utils/getValueLabelObjectsFromEnum";
import { PlainLink } from "../../utils/PlainLink";
import { useGetFilteredCompanies } from "../common/utils/useGetFilteredCompanies";
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

  const attributeSourceQuery = useGetAttributesSource();
  const attributeFormatQuery = useGetAttributesFormat();
  const attributeQualifierQuery = useGetAttributesQualifier();
  const attributeConditionQuery = useGetAttributesCondition();
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
          name={"attributeQualifier"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.qualifier")} error={errors.attributeQualifier}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("attribute.qualifier").toLowerCase() })}
                options={attributeQualifierQuery.data}
                isLoading={attributeQualifierQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={attributeQualifierQuery.data?.find((x) => x.name === value)}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"attributeSource"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.source")} error={errors.attributeSource}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("attribute.source").toLowerCase() })}
                options={attributeSourceQuery.data}
                isLoading={attributeSourceQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={attributeSourceQuery.data?.find((x) => x.name === value)}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"attributeCondition"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.condition")} error={errors.attributeCondition}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("attribute.condition").toLowerCase() })}
                options={attributeConditionQuery.data}
                isLoading={attributeConditionQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={attributeConditionQuery.data?.find((x) => x.name === value)}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"attributeFormat"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.format")} error={errors.attributeFormat}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("attribute.format").toLowerCase() })}
                options={attributeFormatQuery.data}
                isLoading={attributeFormatQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={attributeFormatQuery.data?.find((x) => x.name === value)}
              />
            </FormField>
          )}
        />
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
