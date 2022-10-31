import { Aspect, MimirorgPermission } from "@mimirorg/typelibrary-types";
import { PlainLink } from "common/components/plain-link";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { getValueLabelObjectsFromEnum } from "common/utils/getValueLabelObjectsFromEnum";
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
import { resetSubform } from "features/entities/node/NodeForm.helpers";
import { NodeFormBaseFieldsContainer } from "features/entities/node/NodeFormBaseFields.styled";
import { NodeFormPreview } from "features/entities/node/NodeFormPreview";
import { FormNodeLib } from "features/entities/node/types/formNodeLib";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";

interface NodeFormBaseFieldsProps {
  isPrefilled?: boolean;
}

/**
 * Component which contains all shared fields for variations of the node form.
 *
 * @param isPrefilled
 * @constructor
 */
export const NodeFormBaseFields = ({ isPrefilled }: NodeFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { control, register, resetField, setValue, formState } = useFormContext<FormNodeLib>();
  const { errors } = formState;

  const rdsQuery = useGetRds();
  const symbolQuery = useGetSymbols();
  const purposeQuery = useGetPurposes();
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);
  const companies = useGetFilteredCompanies(MimirorgPermission.Write);

  return (
    <NodeFormBaseFieldsContainer>
      <NodeFormPreview control={control} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("node.name")} error={errors.name}>
          <Input placeholder={t("node.placeholders.name")} {...register("name")} />
        </FormField>
        <FormField label={t("node.purpose")} error={errors.purposeName}>
          <Controller
            control={control}
            name={"purposeName"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("node.purpose").toLowerCase() })}
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
        <FormField label={t("node.aspect")} error={errors.aspect}>
          <Controller
            control={control}
            name={"aspect"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={isPrefilled}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("node.disabled.aspect")}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("node.aspect").toLowerCase() })}
                  options={aspectOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetSubform(resetField);
                    onChange(x?.value);
                  }}
                  value={aspectOptions.find((x) => x.value === value)}
                  isDisabled={isPrefilled}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>
        <FormField label={t("node.symbol")} error={errors.symbol}>
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
        </FormField>
        <Input type={"hidden"} {...register("rdsCode")} />
        <FormField label={t("node.rds")} error={errors.rdsName}>
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
        </FormField>
        <FormField label={t("node.owner")} error={errors.companyId}>
          <Controller
            control={control}
            name={"companyId"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
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
            )}
          />
        </FormField>
        <FormField label={t("node.description")} error={errors.description}>
          <Textarea placeholder={t("node.placeholders.description")} {...register("description")} />
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
    </NodeFormBaseFieldsContainer>
  );
};
