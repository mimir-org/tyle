import { DevTool } from "@hookform/devtools";
import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import { useTheme } from "styled-components";
import { Button } from "../../../../../complib/buttons";
import { Form, FormErrorBanner, FormField, FormFieldset } from "../../../../../complib/form";
import { Input, Select, Textarea } from "../../../../../complib/inputs";
import { MotionFlexbox } from "../../../../../complib/layouts";
import { MotionText, Text } from "../../../../../complib/text";
import { Actionable } from "../../../../../complib/types";
import { useGetCompanies } from "../../../../../data/queries/auth/queriesCompany";
import { useCreateUser } from "../../../../../data/queries/auth/queriesUser";
import { useExecuteOnCriteria } from "../../../../../hooks/useExecuteOnCriteria";
import { useServerValidation } from "../../../../../hooks/useServerValidation";
import { UnauthenticatedContent } from "../../../../app/components/unauthenticated/layout/UnauthenticatedContent";
import { RegisterProcessing } from "./RegisterProcessing";

interface RegisterDetailsProps {
  setUserEmail: (email: string) => void;
  complete?: Partial<Actionable>;
}

export const RegisterDetails = ({ complete, setUserEmail }: RegisterDetailsProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { register, control, handleSubmit, setError, formState } = useForm<MimirorgUserAm>();
  const { errors } = formState;

  const companyQuery = useGetCompanies();
  const mutation = useCreateUser();

  const onSubmit = (data: MimirorgUserAm) => {
    setUserEmail(data.email);
    mutation.mutate(data);
  };

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <UnauthenticatedContent
      title={t("register.details.title")}
      subtitle={t("register.details.description")}
      infoTitle={t("register.details.info.title")}
      infoText={t("register.details.info.text")}
    >
      {mutation.isLoading && <RegisterProcessing />}
      {!mutation.isSuccess && !mutation.isLoading && (
        <Form onSubmit={handleSubmit((data) => onSubmit(data))}>
          {mutation.isError && <FormErrorBanner>{t("register.details.error")}</FormErrorBanner>}
          <FormFieldset>
            <FormField label={`${t("common.fields.company")} *`} error={errors.companyId}>
              <Controller
                control={control}
                name={"companyId"}
                rules={{ required: true }}
                render={({ field: { value, onChange, ref, ...rest } }) => (
                  <Select
                    {...rest}
                    selectRef={ref}
                    placeholder={t("common.placeholders.company")}
                    options={companyQuery?.data}
                    getOptionLabel={(x) => x.name}
                    getOptionValue={(x) => x.id.toString()}
                    onChange={(x) => {
                      onChange(x?.id);
                    }}
                    value={companyQuery.data?.find((x) => x.id === value)}
                  />
                )}
              />
            </FormField>

            <FormField label={`${t("common.fields.email")} *`} error={formState.errors.email}>
              <Input
                id="email"
                type="email"
                placeholder={t("common.placeholders.email")}
                {...register("email", { required: true })}
              />
            </FormField>

            <FormField label={`${t("common.fields.firstname")} *`} error={errors.firstName}>
              <Input
                id="firstName"
                placeholder={t("common.placeholders.firstname")}
                {...register("firstName", { required: true })}
              />
            </FormField>

            <FormField label={`${t("common.fields.lastname")} *`} error={errors.lastName}>
              <Input
                id="lastName"
                placeholder={t("common.placeholders.lastname")}
                {...register("lastName", { required: true })}
              />
            </FormField>

            <FormField label={`${t("common.fields.password")} *`} error={errors.password}>
              <Input
                id="password"
                type="password"
                placeholder={t("common.placeholders.password")}
                {...register("password", { required: true })}
              />
            </FormField>

            <FormField
              label={`${t("common.confirm")} ${t("common.fields.password").toLowerCase()} *`}
              error={errors.confirmPassword}
            >
              <Input
                id="confirmPassword"
                type="password"
                placeholder={t("common.placeholders.password")}
                {...register("confirmPassword", { required: true })}
              />
            </FormField>

            <FormField label={`${t("common.fields.purpose")} *`} error={errors.purpose}>
              <Textarea placeholder={t("common.placeholders.purpose")} {...register("purpose", { required: true })} />
            </FormField>

            <MotionText color={theme.tyle.color.sys.surface.variant.on} layout={"position"} as={"i"}>
              {t("common.placeholders.required")}
            </MotionText>
          </FormFieldset>

          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.xxl}>
            <Button type={"submit"}>{complete?.actionText}</Button>
            <Text color={theme.tyle.color.sys.surface.variant.on}>
              {t("register.details.altLead")} <Link to="/">{t("register.details.altLink")}</Link>
            </Text>
          </MotionFlexbox>
        </Form>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </UnauthenticatedContent>
  );
};
