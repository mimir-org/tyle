import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, Controller, UseFormRegister, UseFormResetField, UseFormSetValue } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Button } from "../../../complib/buttons";
import { Popover } from "../../../complib/data-display";
import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Box, Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { ConditionalWrapper } from "../../../complib/utils";
import { useGetCompanies } from "../../../data/queries/auth/queriesCompany";
import { useGetPurposes } from "../../../data/queries/tyle/queriesPurpose";
import { useGetRds } from "../../../data/queries/tyle/queriesRds";
import { useGetTerminals } from "../../../data/queries/tyle/queriesTerminal";
import { getValueLabelObjectsFromEnum } from "../../../utils/getValueLabelObjectsFromEnum";
import { TerminalButton } from "../../common/terminal";
import { PlainLink } from "../../utils/PlainLink";
import { resetSubform } from "./TransportForm.helpers";
import { TransportFormBaseFieldsContainer } from "./TransportFormBaseFields.styled";
import { TransportFormPreview } from "./TransportFormPreview";
import { FormTransportLib } from "./types/formTransportLib";

interface TransportFormBaseFieldsProps {
  control: Control<FormTransportLib>;
  register: UseFormRegister<FormTransportLib>;
  resetField: UseFormResetField<FormTransportLib>;
  setValue: UseFormSetValue<FormTransportLib>;
  hasPrefilledData?: boolean;
}

/**
 * Component which contains all simple value fields of the transport form.
 *
 * @param control
 * @param register
 * @param resetField
 * @param setValue
 * @param hasPrefilledData
 * @constructor
 */
export const TransportFormBaseFields = ({
  control,
  register,
  resetField,
  setValue,
  hasPrefilledData,
}: TransportFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation();

  const rdsQuery = useGetRds();
  const purposeQuery = useGetPurposes();
  const companyQuery = useGetCompanies();
  const terminalQuery = useGetTerminals();
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);

  return (
    <TransportFormBaseFieldsContainer>
      <TransportFormPreview control={control} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("transport.name")}>
          <Input placeholder={t("transport.placeholders.name")} {...register("name")} />
        </FormField>

        <Controller
          control={control}
          name={"purposeName"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("transport.purpose")}>
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
            <FormField label={t("transport.aspect")}>
              <ConditionalWrapper
                condition={hasPrefilledData}
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
                  isDisabled={hasPrefilledData}
                />
              </ConditionalWrapper>
            </FormField>
          )}
        />

        <Controller
          control={control}
          name={`terminalId`}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("transport.terminal")}>
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
            <FormField label={t("transport.rds")}>
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
            <FormField label={t("transport.owner")}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("transport.owner").toLowerCase() })}
                options={companyQuery.data}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange(x?.id);
                }}
                value={companyQuery.data?.find((x) => x.id === value)}
              />
            </FormField>
          )}
        />

        <FormField label={t("transport.description")}>
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
