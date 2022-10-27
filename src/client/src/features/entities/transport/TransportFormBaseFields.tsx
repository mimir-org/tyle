import { Aspect, MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { Button } from "complib/buttons";
import { Popover } from "complib/data-display";
import { FormField } from "complib/form";
import { Input, Select, Textarea } from "complib/inputs";
import { Box, Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { ConditionalWrapper } from "complib/utils";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { PlainLink } from "../../../common/components/plain-link";
import { TerminalButton } from "../../../common/components/terminal";
import { getValueLabelObjectsFromEnum } from "../../../common/utils/getValueLabelObjectsFromEnum";
import { useGetPurposes } from "../../../data/queries/tyle/queriesPurpose";
import { useGetRds } from "../../../data/queries/tyle/queriesRds";
import { useGetTerminals } from "../../../data/queries/tyle/queriesTerminal";
import { resetSubform } from "./TransportForm.helpers";
import { TransportFormBaseFieldsContainer } from "./TransportFormBaseFields.styled";
import { TransportFormPreview } from "./TransportFormPreview";
import { FormTransportLib } from "./types/formTransportLib";

interface TransportFormBaseFieldsProps {
  isPrefilled?: boolean;
}

/**
 * Component which contains all simple value fields of the transport form.
 *
 * @param isPrefilled
 * @constructor
 */
export const TransportFormBaseFields = ({ isPrefilled }: TransportFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { control, register, resetField, setValue, formState } = useFormContext<FormTransportLib>();
  const { errors } = formState;

  const rdsQuery = useGetRds();
  const purposeQuery = useGetPurposes();
  const terminalQuery = useGetTerminals();
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);
  const companies = useGetFilteredCompanies(MimirorgPermission.Write);

  return (
    <TransportFormBaseFieldsContainer>
      <TransportFormPreview control={control} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("transport.name")} error={errors.name}>
          <Input placeholder={t("transport.placeholders.name")} {...register("name")} />
        </FormField>

        <Controller
          control={control}
          name={"purposeName"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("transport.purpose")} error={errors.purposeName}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("transport.purpose").toLowerCase() })}
                options={purposeQuery.data}
                isLoading={purposeQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.name}
                onChange={(x) => onChange(x?.name)}
                value={purposeQuery.data?.find((x) => x.name === value)}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"aspect"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("transport.aspect")} error={errors.aspect}>
              <ConditionalWrapper
                condition={isPrefilled}
                wrapper={(c) => (
                  <Popover align={"start"} maxWidth={"225px"} content={t("transport.disabled.aspect")}>
                    <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                      {c}
                    </Box>
                  </Popover>
                )}
              >
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={t("common.templates.select", { object: t("transport.aspect").toLowerCase() })}
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
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={`terminalId`}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("transport.terminal")} error={errors.terminalId}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("terminals.name").toLowerCase() })}
                options={terminalQuery.data}
                isLoading={terminalQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id}
                onChange={(x) => {
                  setValue("terminalColor", x?.color, { shouldDirty: true });
                  onChange(x?.id);
                }}
                value={terminalQuery.data?.find((x) => x.id === value)}
                formatOptionLabel={(x) => (
                  <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
                    {x.color && <TerminalButton as={"span"} variant={"small"} color={x.color} />}
                    <Text>{x.name}</Text>
                  </Flexbox>
                )}
              />
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"rdsName"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("transport.rds")} error={errors.rdsName}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("transport.rds").toLowerCase() })}
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
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={"companyId"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("transport.owner")} error={errors.companyId}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("transport.owner").toLowerCase() })}
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

        <FormField label={t("transport.description")} error={errors.description}>
          <Textarea placeholder={t("transport.placeholders.description")} {...register("description")} />
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
    </TransportFormBaseFieldsContainer>
  );
};
