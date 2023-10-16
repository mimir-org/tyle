import {
  Button,
  Checkbox,
  ConditionalWrapper,
  FormBaseFieldsContainer,
  FormField,
  Input,
  Select,
  VisuallyHidden,
} from "@mimirorg/component-library";
import { Controller, useFieldArray, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { ConstraintType } from "common/types/attributes/constraintType";
import { getOptionsFromEnum, Option } from "common/utils/getOptionsFromEnum";
import { XsdDataType } from "common/types/attributes/xsdDataType";
import { FormSection } from "../common/form-section/FormSection";
import { PlusSmall, Trash } from "@styled-icons/heroicons-outline";
import { AttributeFormFields } from "./AttributeForm.helpers";

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */

export const ValueConstraintForm = () => {
  const { t } = useTranslation("entities");
  const {
    control,
    register,
    setValue,
    formState: { errors },
  } = useFormContext<AttributeFormFields>();

  const constraintTypeOptions = getOptionsFromEnum<ConstraintType>(ConstraintType);
  const dataTypeOptions = getOptionsFromEnum<XsdDataType>(XsdDataType);
  const booleanOptions: Option<string>[] = [
    { value: "true", label: "True" },
    { value: "false", label: "False" },
  ];

  const valueConstraint = useWatch({ control, name: "valueConstraint" });
  const chosenConstraintType = useWatch({ control, name: "constraintType" });
  const chosenDataType = useWatch({ control, name: "dataType" });

  const resetValueConstraintFields = (constraintType: ConstraintType | undefined) => {
    setValue("value", undefined);
    setValue("valueList", []);
    setValue("pattern", undefined);
    setValue("minValue", undefined);
    setValue("maxValue", undefined);

    if (constraintType === ConstraintType.IsInListOfAllowedValues && chosenDataType === XsdDataType.Boolean) {
      setValue("dataType", XsdDataType.String);
    } else if (
      constraintType === ConstraintType.IsInNumberRange &&
      chosenDataType !== XsdDataType.Decimal &&
      chosenDataType !== XsdDataType.Integer
    ) {
      setValue("dataType", XsdDataType.Decimal);
    } else if (constraintType === ConstraintType.MatchesRegexPattern) {
      setValue("dataType", XsdDataType.String);
    }

    if (constraintType === undefined) {
      setValue("constraintType", undefined);
      setValue("dataType", undefined);
      setValue("requireValue", false);
    }
  };

  const getDataTypeOptions = () => {
    if (chosenConstraintType === ConstraintType.IsInListOfAllowedValues) {
      return dataTypeOptions.filter((x) => x.value !== XsdDataType.Boolean);
    } else if (chosenConstraintType === ConstraintType.IsInNumberRange) {
      return dataTypeOptions.filter((x) => x.value === XsdDataType.Decimal || x.value === XsdDataType.Integer);
    } else if (chosenConstraintType === ConstraintType.MatchesRegexPattern) {
      return dataTypeOptions.filter((x) => x.value === XsdDataType.String);
    }
    return dataTypeOptions;
  };

  const valueListEntries = useFieldArray({ control, name: "valueList" });

  return (
    <FormBaseFieldsContainer>
      <FormField label={t("attribute.valueConstraint.title")}>
        <Controller
          control={control}
          name={"valueConstraint"}
          render={({ field: { value, onChange, ...rest } }) => (
            <Checkbox
              {...rest}
              checked={value}
              onCheckedChange={(checked) => {
                if (!checked) resetValueConstraintFields(undefined);
                onChange(checked);
              }}
            />
          )}
        />
      </FormField>

      <ConditionalWrapper condition={!valueConstraint} wrapper={(c) => <VisuallyHidden>{c}</VisuallyHidden>}>
        <>
          <FormField label={t("attribute.valueConstraint.constraintType")} error={errors.constraintType}>
            <Controller
              control={control}
              name={"constraintType"}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", {
                    object: t("attribute.valueConstraint.constraintType").toLowerCase(),
                  })}
                  options={constraintTypeOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetValueConstraintFields(x?.value);
                    onChange(x?.value);
                  }}
                  value={constraintTypeOptions.find((x) => x.value === value)}
                />
              )}
            />
          </FormField>

          <FormField label={t("attribute.valueConstraint.dataType")} error={errors.dataType}>
            <Controller
              control={control}
              name={"dataType"}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", {
                    object: t("attribute.valueConstraint.dataType").toLowerCase(),
                  })}
                  options={getDataTypeOptions()}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    onChange(x?.value);
                  }}
                  value={dataTypeOptions.find((x) => x.value === value)}
                />
              )}
            />
          </FormField>

          <ConditionalWrapper
            condition={chosenConstraintType === ConstraintType.HasSpecificValue || chosenConstraintType === undefined}
            wrapper={(c) => <VisuallyHidden>{c}</VisuallyHidden>}
          >
            <FormField label={t("attribute.valueConstraint.requireValue")} error={errors.requireValue}>
              <Controller
                control={control}
                name={"requireValue"}
                render={({ field: { value, onChange, ...rest } }) => (
                  <Checkbox {...rest} checked={value} onCheckedChange={onChange} />
                )}
              />
            </FormField>
          </ConditionalWrapper>

          <ConditionalWrapper
            condition={chosenConstraintType !== ConstraintType.HasSpecificValue}
            wrapper={(c) => <VisuallyHidden>{c}</VisuallyHidden>}
          >
            <FormField label={t("attribute.valueConstraint.value")} error={errors.value}>
              {chosenDataType === XsdDataType.Boolean ? (
                <Controller
                  control={control}
                  name={"value"}
                  render={({ field: { value, onChange, ref, ...rest } }) => (
                    <Select
                      {...rest}
                      selectRef={ref}
                      placeholder={t("common.templates.select", {
                        object: t("attribute.valueConstraint.value").toLowerCase(),
                      })}
                      options={booleanOptions}
                      getOptionLabel={(x) => x.label}
                      onChange={(x) => {
                        onChange(x?.value);
                      }}
                      value={booleanOptions.find((x) => x.value === value)}
                    />
                  )}
                />
              ) : (
                <Input placeholder={t("attribute.placeholders.valueConstraint.value")} {...register("value")} />
              )}
            </FormField>
          </ConditionalWrapper>

          <ConditionalWrapper
            condition={chosenConstraintType !== ConstraintType.IsInListOfAllowedValues}
            wrapper={(c) => <VisuallyHidden>{c}</VisuallyHidden>}
          >
            <FormSection
              title={t("attribute.valueConstraint.valueList.title")}
              error={errors.valueList?.root}
              action={
                <Button icon={<PlusSmall />} iconOnly onClick={() => valueListEntries.append({ value: "" })}>
                  {t("attribute.valueConstraint.valueList.add")}
                </Button>
              }
            >
              {valueListEntries.fields.map((field, index) => (
                <FormField
                  key={field.id}
                  error={errors.valueList && !errors.valueList.root ? errors.valueList[index]?.value : undefined}
                >
                  <Input {...register(`valueList.${index}.value`)} />
                  <Button icon={<Trash />} iconOnly onClick={() => valueListEntries.remove(index)}>
                    {t("attribute.valueConstraint.valueList.remove")}
                  </Button>
                </FormField>
              ))}
            </FormSection>
          </ConditionalWrapper>

          <ConditionalWrapper
            condition={chosenConstraintType !== ConstraintType.MatchesRegexPattern}
            wrapper={(c) => <VisuallyHidden>{c}</VisuallyHidden>}
          >
            <FormField label={t("attribute.valueConstraint.pattern")} error={errors.pattern}>
              <Input placeholder={t("attribute.placeholders.valueConstraint.pattern")} {...register("pattern")} />
            </FormField>
          </ConditionalWrapper>

          <ConditionalWrapper
            condition={chosenConstraintType !== ConstraintType.IsInNumberRange}
            wrapper={(c) => <VisuallyHidden>{c}</VisuallyHidden>}
          >
            <>
              <FormField label={t("attribute.valueConstraint.minValue")} error={errors.minValue}>
                <Input placeholder={t("attribute.placeholders.valueConstraint.minValue")} {...register("minValue")} />
              </FormField>

              <FormField label={t("attribute.valueConstraint.maxValue")} error={errors.maxValue}>
                <Input placeholder={t("attribute.placeholders.valueConstraint.maxValue")} {...register("maxValue")} />
              </FormField>
            </>
          </ConditionalWrapper>
        </>
      </ConditionalWrapper>
    </FormBaseFieldsContainer>
  );
};
