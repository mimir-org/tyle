import { Aspect, AttributeType, Discipline } from "@mimirorg/typelibrary-types";
import { Control, Controller, UseFormRegister, UseFormResetField, UseFormSetValue } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Button } from "../../../complib/buttons";
import { Popover } from "../../../complib/data-display";
import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Box, Flexbox } from "../../../complib/layouts";
import { Icon } from "../../../complib/media";
import { Text } from "../../../complib/text";
import { ConditionalWrapper } from "../../../complib/utils";
import { useGetCompanies } from "../../../data/queries/auth/queriesCompany";
import { useGetPurposes } from "../../../data/queries/tyle/queriesPurpose";
import { useGetRds } from "../../../data/queries/tyle/queriesRds";
import { useGetSymbols } from "../../../data/queries/tyle/queriesSymbol";
import { getValueLabelObjectsFromEnum } from "../../../utils/getValueLabelObjectsFromEnum";
import { PlainLink } from "../../utils/PlainLink";
import { prepareParentAttributes, resetSubform } from "./AttributeForm.helpers";
import { AttributeFormBaseFieldsContainer } from "./AttributeFormBaseFields.styled";
import { FormAttributeLib } from "./types/formAttributeLib";
import { Select as AttributeSelect } from "@mimirorg/typelibrary-types";
import {
  useGetAttributes,
  useGetAttributesCondition,
  useGetAttributesFormat,
  useGetAttributesQualifier,
  useGetAttributesSource,
} from "../../../data/queries/tyle/queriesAttribute";

interface AttributeFormBaseFieldsProps {
  control: Control<FormAttributeLib>;
  register: UseFormRegister<FormAttributeLib>;
  resetField: UseFormResetField<FormAttributeLib>;
  setValue: UseFormSetValue<FormAttributeLib>;
  hasPrefilledData?: boolean;
}

/**
 * Component which contains all shared fields for variations of the node form.
 *
 * @param control
 * @param register
 * @param resetField
 * @param setValue
 * @param hasPrefilledData
 * @constructor
 */
export const AttributeFormBaseFields = ({
  control,
  register,
  resetField,
  setValue,
  hasPrefilledData,
}: AttributeFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation();

  const rdsQuery = useGetRds();
  const symbolQuery = useGetSymbols();
  const attributeQualifierQuery = useGetAttributesQualifier();
  const attributeSourceQuery = useGetAttributesSource();
  const attributeConditionQuery = useGetAttributesCondition();
  const attributeFormatQuery = useGetAttributesFormat();
  const companyQuery = useGetCompanies();
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);
  const disciplineOptions = getValueLabelObjectsFromEnum<Discipline>(Discipline);
  const selectOptions = getValueLabelObjectsFromEnum<AttributeSelect>(AttributeSelect);
  const typeOptions = getValueLabelObjectsFromEnum<AttributeType>(AttributeType);
  const attributeQuery = useGetAttributes();
  const filteredAttributes = prepareParentAttributes(attributeQuery.data);

  return (
    <AttributeFormBaseFieldsContainer>
      {/* <NodeFormPreview control={control} /> */}

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("attribute.name")}>
          <Input placeholder={t("attribute.placeholders.name")} {...register("name")} />
        </FormField>

        <FormField label={t("attribute.aspect")}>
          <Controller
            control={control}
            name={"aspect"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={hasPrefilledData}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("attribute.disabled.aspect")}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("attribute.aspect").toLowerCase() })}
                  options={aspectOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetSubform(resetField);
                    onChange(x?.value);
                  }}
                  value={aspectOptions.find((x) => x.value === value)}
                  isDisabled={hasPrefilledData}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>

        <FormField label={t("attribute.discipline")}>
          <Controller
            control={control}
            name={"discipline"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={hasPrefilledData}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("attribute.disabled.discipline")}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("attribute.discipline").toLowerCase() })}
                  options={disciplineOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetSubform(resetField);
                    onChange(x?.value);
                  }}
                  value={disciplineOptions.find((x) => x.value === value)}
                  isDisabled={hasPrefilledData}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>

        <FormField label={t("attribute.select")}>
          <Controller
            control={control}
            name={"select"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={hasPrefilledData}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("attribute.disabled.select")}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", {
                    object: t("attribute.placeholders.select").toLowerCase(),
                  })}
                  options={selectOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetSubform(resetField);
                    onChange(x?.value);
                  }}
                  value={selectOptions.find((x) => x.value === value)}
                  isDisabled={hasPrefilledData}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>

        <FormField label={t("attribute.qualifier")}>
          <Controller
            control={control}
            name={"attributeQualifier"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
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
            )}
          />
        </FormField>

        <FormField label={t("attribute.source")}>
          <Controller
            control={control}
            name={"attributeSource"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
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
            )}
          />
        </FormField>

        <FormField label={t("attribute.condition")}>
          <Controller
            control={control}
            name={"attributeCondition"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
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
            )}
          />
        </FormField>

        <FormField label={t("attribute.format")}>
          <Controller
            control={control}
            name={"attributeFormat"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
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
            )}
          />
        </FormField>

        <FormField label={t("attribute.type")}>
          <Controller
            control={control}
            name={"attributeType"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={hasPrefilledData}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("attribute.disabled.type")}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("attribute.type").toLowerCase() })}
                  options={typeOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetSubform(resetField);
                    onChange(x?.value);
                  }}
                  value={typeOptions.find((x) => x.value === value)}
                  isDisabled={hasPrefilledData}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>

        <FormField label={t("attribute.parent")}>
          <Controller
            control={control}
            name={"name"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={hasPrefilledData}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("attribute.disabled.parent")}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("attribute.parentIri").toLowerCase() })}
                  options={filteredAttributes}
                  getOptionLabel={(x) => x.name}
                  onChange={(x) => {
                    resetSubform(resetField);
                    onChange(x?.parentIri);
                  }}
                  value={filteredAttributes.find((x) => x.name === value)}
                  isDisabled={hasPrefilledData}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>

        {/* <FormField label={t("node.symbol")}>
          <Controller
            control={control}
            name={"symbol"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("node.symbol").toLowerCase() })}
                options={symbolQuery.data}
                isLoading={symbolQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.data}
                onChange={(x) => onChange(x?.data)}
                value={symbolQuery.data?.find((x) => x.data === value)}
                formatOptionLabel={(x) => (
                  <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
                    <Icon src={x.data} />
                    <Text>{x.name}</Text>
                  </Flexbox>
                )}
              />
            )}
          />
        </FormField> */}
        {/* <Input type={"hidden"} {...register("rdsCode")} />
        <FormField label={t("node.rds")}>
          <Controller
            control={control}
            name={"rdsName"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("node.rds").toLowerCase() })}
                options={rdsQuery.data}
                isLoading={rdsQuery.isLoading}
                getOptionLabel={(x) => `${x.id} - ${x.name}`}
                getOptionValue={(x) => x.id}
                value={rdsQuery.data?.find((x) => x.name === value)}
                onChange={(rds) => {
                  if (rds) {
                    setValue("rdsCode", rds.id, { shouldDirty: true });
                    onChange(rds.name);
                  }
                }}
              />
            )}
          />
        </FormField> */}
        {/* <FormField label={t("node.owner")}>
          <Controller
            control={control}
            name={"companyId"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("node.owner").toLowerCase() })}
                options={companyQuery.data}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange(x?.id);
                }}
                value={companyQuery.data?.find((x) => x.id === value)}
              />
            )}
          />
        </FormField> */}
        {/* <FormField label={t("node.description")}>
          <Textarea placeholder={t("node.placeholders.description")} {...register("description")} />
        </FormField> */}
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
