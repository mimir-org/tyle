import { Aspect, MimirorgPermission, State } from "@mimirorg/typelibrary-types";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import {
  Box,
  Button,
  ConditionalWrapper,
  Flexbox,
  FormBaseFieldsContainer,
  FormField,
  Icon,
  Input,
  Popover,
  Select,
  Text,
  Textarea,
} from "@mimirorg/component-library";
import { useGetPurposes } from "external/sources/purpose/purpose.queries";
import { useGetAllRds } from "external/sources/rds/rds.queries";
import { useGetSymbols } from "external/sources/symbol/symbol.queries";
import { PlainLink } from "features/common/plain-link";
import { resetSubform } from "features/entities/block/BlockForm.helpers";
import { BlockFormPreview } from "features/entities/entityPreviews/block/BlockFormPreview";
import { FormBlockLib } from "features/entities/block/types/formBlockLib";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { FormMode } from "../types/formMode";

interface BlockFormBaseFieldsProps {
  isFirstDraft?: boolean;
  mode?: FormMode;
  state?: State;
}

/**
 * Component which contains all shared fields for variations of the block form.
 *
 * @param isFirstDraft
 * @param mode
 * @param state
 * @constructor
 */
export const BlockFormBaseFields = ({ isFirstDraft, mode, state }: BlockFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, resetField, formState } = useFormContext<FormBlockLib>();
  const { errors } = formState;

  const rdsQuery = useGetAllRds();
  const symbolQuery = useGetSymbols();
  const purposeQuery = useGetPurposes();
  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);
  const companies = useGetFilteredCompanies(MimirorgPermission.Write);

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("block.title")}</Text>
      <BlockFormPreview control={control} />

      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
        <FormField label={t("block.name")} error={errors.name}>
          <Input placeholder={t("block.placeholders.name")} {...register("name")} />
        </FormField>
        <FormField label={t("block.purpose")} error={errors.purposeName}>
          <Controller
            control={control}
            name={"purposeName"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("block.purpose").toLowerCase() })}
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
        <FormField label={t("block.aspect")} error={errors.aspect}>
          <Controller
            control={control}
            name={"aspect"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <ConditionalWrapper
                condition={!isFirstDraft}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("block.disabled.aspect")}>
                    <Box borderRadius={theme.mimirorg.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("block.aspect").toLowerCase() })}
                  options={aspectOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => {
                    resetSubform(resetField, x?.value);
                    onChange(x?.value);
                  }}
                  value={aspectOptions.find((x) => x.value === value)}
                  isDisabled={!isFirstDraft}
                />
              </ConditionalWrapper>
            )}
          />
        </FormField>
        <FormField label={t("block.symbol")} error={errors.symbol}>
          <Controller
            control={control}
            name={"symbol"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("block.symbol").toLowerCase() })}
                options={symbolQuery.data}
                isLoading={symbolQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.data}
                onChange={(x) => onChange(x?.data)}
                value={symbolQuery.data?.find((x) => x.data === value)}
                formatOptionLabel={(x) => (
                  <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
                    <Icon src={x.data} />
                    <Text>{x.name}</Text>
                  </Flexbox>
                )}
              />
            )}
          />
        </FormField>
        <Input type={"hidden"} {...register("rdsId")} />
        <FormField label={t("block.rds")} error={errors.rdsId}>
          <Controller
            control={control}
            name={"rdsId"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("block.rds").toLowerCase() })}
                options={rdsQuery.data}
                isLoading={rdsQuery.isLoading}
                getOptionLabel={(x) => `${x.rdsCode} - ${x.name}`}
                getOptionValue={(x) => x.id}
                value={rdsQuery.data?.find((x) => x.id === value)}
                onChange={(rds) => {
                  onChange(rds?.id);
                }}
              />
            )}
          />
        </FormField>
        <FormField label={t("block.owner")} error={errors.companyId}>
          <Controller
            control={control}
            name={"companyId"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("block.owner").toLowerCase() })}
                options={companies}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange(x?.id);
                }}
                isDisabled={!isFirstDraft}
                value={companies.find((x) => x.id === value)}
              />
            )}
          />
        </FormField>
        <FormField label={t("block.description")} error={errors.description}>
          <Textarea placeholder={t("block.placeholders.description")} {...register("description")} />
        </FormField>
      </Flexbox>

      <Flexbox justifyContent={"center"} gap={theme.mimirorg.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>
          {mode === "edit"
            ? state === State.Approved
              ? t("block.createDraft")
              : t("common.edit")
            : t("common.submit")}
        </Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};
