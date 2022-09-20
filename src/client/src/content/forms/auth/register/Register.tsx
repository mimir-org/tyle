import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import { useTheme } from "styled-components";
import { Button } from "../../../../complib/buttons";
import { Form, FormErrorBanner, FormField, FormFieldset, FormHeader } from "../../../../complib/form";
import { Input, Select, Textarea } from "../../../../complib/inputs";
import { MotionFlexbox } from "../../../../complib/layouts";
import { MotionText, Text } from "../../../../complib/text";
import { useGetCompanies } from "../../../../data/queries/auth/queriesCompany";
import { useCreateUser } from "../../../../data/queries/auth/queriesUser";
import { useServerValidation } from "../../../../hooks/useServerValidation";
import { MotionLogo } from "../../../common/logo/Logo";
import { RegisterFinalize } from "./components/RegisterFinalize";
import { RegisterProcessing } from "./components/RegisterProcessing";

export const Register = () => {
  const theme = useTheme();
  const { t } = useTranslation();

  const { register, control, handleSubmit, setError, formState } = useForm<MimirorgUserAm>();
  const { errors } = formState;

  const companyQuery = useGetCompanies();

  const mutation = useCreateUser();
  useServerValidation(mutation.error, setError);

  return (
    <>
      {mutation.isLoading && <RegisterProcessing />}
      {mutation.isSuccess && <RegisterFinalize qrCodeBase64={mutation?.data?.code} />}
      {!mutation.isSuccess && !mutation.isLoading && (
        <Form onSubmit={handleSubmit((data) => mutation.mutate(data))}>
          <MotionLogo layout width={"100px"} height={"50px"} inverse alt="" />
          <FormHeader title={t("forms.register.title")} subtitle={t("forms.register.description")} />

          {mutation.isError && <FormErrorBanner>{t("forms.register.error")}</FormErrorBanner>}

          <FormFieldset>
            <FormField label={`${t("forms.fields.company")} *`} error={errors.companyId}>
              <Controller
                control={control}
                name={"companyId"}
                rules={{ required: true }}
                render={({ field: { value, onChange, ref, ...rest } }) => (
                  <Select
                    {...rest}
                    selectRef={ref}
                    placeholder={t("forms.placeholders.company")}
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

            <FormField label={`${t("forms.fields.email")} *`} error={formState.errors.email}>
              <Input
                id="email"
                type="email"
                placeholder={t("forms.placeholders.email")}
                {...register("email", { required: true })}
              />
            </FormField>

            <FormField label={`${t("forms.fields.firstname")} *`} error={errors.firstName}>
              <Input
                id="firstName"
                placeholder={t("forms.placeholders.firstname")}
                {...register("firstName", { required: true })}
              />
            </FormField>

            <FormField label={`${t("forms.fields.lastname")} *`} error={errors.lastName}>
              <Input
                id="lastName"
                placeholder={t("forms.placeholders.lastname")}
                {...register("lastName", { required: true })}
              />
            </FormField>

            <FormField label={`${t("forms.fields.password")} *`} error={errors.password}>
              <Input
                id="password"
                type="password"
                placeholder={t("forms.placeholders.password")}
                {...register("password", { required: true })}
              />
            </FormField>

            <FormField
              label={`${t("common.confirm")} ${t("forms.fields.password").toLowerCase()} *`}
              error={errors.confirmPassword}
            >
              <Input
                id="confirmPassword"
                type="password"
                placeholder={t("forms.placeholders.password")}
                {...register("confirmPassword", { required: true })}
              />
            </FormField>

            <FormField label={`${t("forms.fields.purpose")} *`} error={errors.purpose}>
              <Textarea placeholder={t("forms.placeholders.purpose")} {...register("purpose", { required: true })} />
            </FormField>

            <MotionText color={theme.tyle.color.sys.surface.variant.on} layout={"position"} as={"i"}>
              {t("forms.placeholders.required")}
            </MotionText>
          </FormFieldset>

          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.xxl}>
            <Button type={"submit"}>{t("forms.register.submit")}</Button>
            <Text color={theme.tyle.color.sys.surface.variant.on}>
              {t("forms.register.altLead")} <Link to="/">{t("forms.register.altLink")}</Link>
            </Text>
          </MotionFlexbox>
        </Form>
      )}
    </>
  );
};
