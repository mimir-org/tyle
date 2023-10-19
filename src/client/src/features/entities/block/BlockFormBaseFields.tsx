import { MimirorgPermission, State } from "@mimirorg/typelibrary-types";
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
//import { PlainLink } from "features/common/plain-link";
import { BlockFormFields } from "features/entities/block/BlockForm.helpers";
// import { BlockFormPreview } from "features/entities/entityPreviews/block/BlockFormPreview";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { FormMode } from "../types/formMode";
import { Aspect } from "common/types/common/aspect";
import { PlainLink } from "features/common/plain-link/PlainLink";

interface BlockFormBaseFieldsProps {
  mode?: FormMode;
  state?: State;
  limited?: boolean;
}

/**
 * Component which contains all shared fields for variations of the block form.
 *
 * @param isFirstDraft
 * @param mode
 * @param state
 *  * @param limited
 * @constructor
 */
export const BlockFormBaseFields = ({ mode, limited }: BlockFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState } = useFormContext<BlockFormFields>();
  const { errors } = formState;

  const rdsQuery = useGetAllRds();
  const symbolQuery = useGetSymbols();
  const purposeQuery = useGetPurposes();
  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);
  const companies = useGetFilteredCompanies(MimirorgPermission.Write);

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("block.title")}</Text>
      {/* <BlockFormPreview control={control} /> */}

      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
        <FormField label={t("block.name")} error={errors.name}>
          <Input placeholder={t("block.placeholders.name")} {...register("name")} />
        </FormField>
        <FormField label={t("block.purpose")} error={errors.purposeId}>
          <Controller
            control={control}
            name={"purposeId"}
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
                value={purposeQuery.data?.find((x) => x.id === value)}
              />
            )}
          />
        </FormField>
        <FormField label={t("block.aspect")} error={errors.aspect}>
          <Controller
            control={control}
            name={"aspect"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("block.aspect").toLowerCase() })}
                options={aspectOptions}
                getOptionLabel={(x) => x.label}
                onChange={(x) => {
                  // resetSubform(resetField, x?.value);
                  onChange(x?.value);
                }}
                value={aspectOptions.find((x) => x.value === value)}
              />
            )}
          />
        </FormField>
        <FormField label={t("terminal.symbol")} error={errors.symbol}>
          <Input placeholder={t("terminal.placeholders.symbol")} {...register("symbol")} disabled={limited} />
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
        <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};
