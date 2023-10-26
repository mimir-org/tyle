import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useExecuteOnCriteria } from "common/hooks/useExecuteOnCriteria";
import {
  Actionable,
  Button,
  Form,
  FormField,
  FormFieldset,
  Input,
  MotionFlexbox,
  MotionText,
  Select,
  Text,
  Textarea,
} from "@mimirorg/component-library";
import { useGetCompanies } from "external/sources/company/company.queries";
import { useCreateUser } from "external/sources/user/user.queries";
import { AuthContent } from "features/auth/common/auth-content/AuthContent";
import { Error } from "features/auth/common/error/Error";
import { Processing } from "features/auth/common/processing/Processing";
import { registerDetailsSchema } from "features/auth/register/details/registerDetailsSchema";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import { useTheme } from "styled-components";
import { MimirorgUserAmCorrectTypes } from "features/auth/common/types/mimirorgUserAm";

interface RegisterDetailsProps {
  setUserEmail: (email: string) => void;
  complete?: Partial<Actionable>;
}

export const RegisterDetails = ({ complete, setUserEmail }: RegisterDetailsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("auth");

  const mutation = useCreateUser();
  const companyQuery = useGetCompanies();
  const companiesAreAvailable = (companyQuery.data && companyQuery.data.length > 0) ?? false;

  const formMethods = useForm({
    resolver: yupResolver(registerDetailsSchema(t, companiesAreAvailable)),
  });

  const { register, control, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const onSubmit = (data: MimirorgUserAmCorrectTypes) => {
    setUserEmail(data.email);
    mutation.mutate({
      ...data,
      purpose: data.purpose ?? "",
      companyId: data.companyId ?? 0,
    });
  };

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <AuthContent
      title={t("register.details.title")}
      subtitle={t("register.details.description")}
      firstRow={
        <>
          {mutation.isLoading && <Processing>{t("register.processing")}</Processing>}
          {!mutation.isSuccess && !mutation.isLoading && (
            <Form id={"details-form"} onSubmit={handleSubmit((data) => onSubmit(data))}>
              {mutation.isError && <Error>{t("register.details.error")}</Error>}
              <FormFieldset>
                <FormField label={`${t("register.details.company")} *`} error={errors.companyId}>
                  <Controller
                    control={control}
                    name={"companyId"}
                    render={({ field: { value, onChange, ref, ...rest } }) => (
                      <Select
                        {...rest}
                        selectRef={ref}
                        placeholder={t("register.details.placeholders.company")}
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

                <FormField label={`${t("register.details.email")} *`} error={formState.errors.email}>
                  <Input
                    id="email"
                    type="email"
                    placeholder={t("register.details.placeholders.email")}
                    {...register("email")}
                  />
                </FormField>

                <FormField label={`${t("register.details.firstname")} *`} error={errors.firstName}>
                  <Input
                    id="firstName"
                    placeholder={t("register.details.placeholders.firstname")}
                    {...register("firstName")}
                  />
                </FormField>

                <FormField label={`${t("register.details.lastname")} *`} error={errors.lastName}>
                  <Input
                    id="lastName"
                    placeholder={t("register.details.placeholders.lastname")}
                    {...register("lastName")}
                  />
                </FormField>

                <FormField label={`${t("register.details.password")} *`} error={errors.password}>
                  <Input
                    id="password"
                    type="password"
                    placeholder={t("register.details.placeholders.password")}
                    {...register("password")}
                  />
                </FormField>

                <FormField label={`${t("register.details.confirmPassword")} *`} error={errors.confirmPassword}>
                  <Input
                    id="confirmPassword"
                    type="password"
                    placeholder={t("register.details.placeholders.password")}
                    {...register("confirmPassword")}
                  />
                </FormField>

                <FormField label={`${t("register.details.purpose")} *`} error={errors.purpose}>
                  <Textarea placeholder={t("register.details.placeholders.purpose")} {...register("purpose")} />
                </FormField>

                <MotionText color={theme.mimirorg.color.surface.variant.on} layout={"position"} as={"i"}>
                  {t("register.details.placeholders.required")}
                </MotionText>
              </FormFieldset>
            </Form>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("register.details.info.text")}</Text>
          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.mimirorg.spacing.xxl}>
            <Button type={"submit"} form={"details-form"}>
              {complete?.actionText}
            </Button>
            <Text color={theme.mimirorg.color.surface.variant.on}>
              {t("register.details.altLead")} <Link to="/">{t("register.details.altLink")}</Link>
            </Text>
          </MotionFlexbox>
        </>
      }
    />
  );
};
