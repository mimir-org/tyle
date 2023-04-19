import { Aspect, MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { Button } from "complib/buttons";
import { Popover } from "complib/data-display";
import { FormField } from "complib/form";
import { Input, Select, Textarea } from "complib/inputs";
import { Box, Flexbox } from "complib/layouts";
import { Icon } from "complib/media";
import { Text } from "complib/text";
import { ConditionalWrapper } from "complib/utils";
import { useGetPurposes } from "external/sources/purpose/purpose.queries";
import { useGetRds } from "external/sources/rds/rds.queries";
import { useGetSymbols } from "external/sources/symbol/symbol.queries";
import { PlainLink } from "features/common/plain-link";
import { resetSubform } from "features/entities/aspectobject/AspectObjectForm.helpers";
import { AspectObjectFormBaseFieldsContainer } from "features/entities/aspectobject/AspectObjectFormBaseFields.styled";
import { AspectObjectFormPreview } from "features/entities/aspectobject/AspectObjectFormPreview";
import { FormAspectObjectLib } from "features/entities/aspectobject/types/formAspectObjectLib";
import { AspectObjectFormMode } from "features/entities/aspectobject/types/aspectObjectFormMode";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";

interface AspectObjectFormBaseFieldsProps {
  mode?: AspectObjectFormMode;
}

/**
 * Component which contains all shared fields for variations of the aspect object form.
 *
 * @param mode
 * @constructor
 */
export const AspectObjectFormBaseFields = ({ mode }: AspectObjectFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, resetField, setValue, formState } = useFormContext<FormAspectObjectLib>();
  const { errors } = formState;

  const rdsQuery = useGetRds();
  const symbolQuery = useGetSymbols();
  const purposeQuery = useGetPurposes();
  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);
  const companies = useGetFilteredCompanies(MimirorgPermission.Write);

  return (
    <AspectObjectFormBaseFieldsContainer>
      <AspectObjectFormPreview control={control} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("aspectObject.name")} error={errors.name}>
          <Input placeholder={t("aspectObject.placeholders.name")} {...register("name")} disabled={mode === "edit"} />
        </FormField>
        <FormField label={t("aspectObject.purpose")} error={errors.purposeName}>
          <Controller
            control={control}
            name={"purposeName"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("aspectObject.purpose").toLowerCase() })}
                options={purposeQuery.data}
                isLoading={purposeQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={purposeQuery.data?.find((x) => x.name === value)}
              />
            )}
          />
        </FormField>
        <FormField label={t("aspectObject.aspect")} error={errors.aspect}>
          <Controller
            control={control}
            name={"aspect"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={mode === "edit"}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("aspectObject.disabled.aspect")}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("aspectObject.aspect").toLowerCase() })}
                  options={aspectOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetSubform(resetField);
                    onChange(x?.value);
                  }}
                  value={aspectOptions.find((x) => x.value === value)}
                  isDisabled={mode === "edit"}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>
        <FormField label={t("aspectObject.symbol")} error={errors.symbol}>
          <Controller
            control={control}
            name={"symbol"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("aspectObject.symbol").toLowerCase() })}
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
        </FormField>
        <Input type={"hidden"} {...register("rdsId")} />
        <FormField label={t("aspectObject.rds")} error={errors.rdsId}>
          <Controller
            control={control}
            name={"rdsId"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("aspectObject.rds").toLowerCase() })}
                options={rdsQuery.data}
                isLoading={rdsQuery.isLoading}
                getOptionLabel={(x) => `${x.rdsCode} - ${x.name}`}
                getOptionValue={(x) => x.id}
                value={rdsQuery.data?.find((x) => x.id === value)}
                onChange={(rds) => {
                  onChange(rds?.id);
                }}
                isDisabled={mode === "edit"}
              />
            )}
          />
        </FormField>
        <FormField label={t("aspectObject.owner")} error={errors.companyId}>
          <Controller
            control={control}
            name={"companyId"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("aspectObject.owner").toLowerCase() })}
                options={companies}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange(x?.id);
                }}
                isDisabled={mode === "edit"}
                value={companies.find((x) => x.id === value)}
              />
            )}
          />
        </FormField>
        <FormField label={t("aspectObject.description")} error={errors.description}>
          <Textarea placeholder={t("aspectObject.placeholders.description")} {...register("description")} />
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
    </AspectObjectFormBaseFieldsContainer>
  );
};
