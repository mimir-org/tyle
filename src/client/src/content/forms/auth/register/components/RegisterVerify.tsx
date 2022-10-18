import { DevTool } from "@hookform/devtools";
import { MimirorgQrCodeCm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Button } from "../../../../../complib/buttons";
import { FormErrorBanner } from "../../../../../complib/form";
import { Digits, Input } from "../../../../../complib/inputs";
import { Flexbox } from "../../../../../complib/layouts";
import { Actionable } from "../../../../../complib/types";
import { useGenerateMfa, useVerification } from "../../../../../data/queries/auth/queriesUser";
import { useExecuteOnCriteria } from "../../../../../hooks/useExecuteOnCriteria";
import { UnauthenticatedContent } from "../../../../app/components/unauthenticated/layout/UnauthenticatedContent";
import { RegisterProcessing } from "./RegisterProcessing";
import { MotionRegisterVerifyForm } from "./RegisterVerify.styled";

type RegisterVerifyProps = Pick<MimirorgVerifyAm, "email"> & {
  setQrCodeInfo: (info: MimirorgQrCodeCm) => void;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

export const RegisterVerify = ({ email, setQrCodeInfo, cancel, complete }: RegisterVerifyProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { control, register, handleSubmit } = useForm<MimirorgVerifyAm>();

  const generateMfaMutation = useGenerateMfa();
  const verificationMutation = useVerification();

  const showError = generateMfaMutation.isError || verificationMutation.isError;
  const showProcessing = verificationMutation.isLoading || generateMfaMutation.isLoading;
  const showInput = !verificationMutation.isSuccess && !verificationMutation.isLoading;

  const onSubmit = async (data: MimirorgVerifyAm) => {
    const verified = await verificationMutation.mutateAsync(data);
    const qrCodeInfo = await generateMfaMutation.mutateAsync(data);
    verified && qrCodeInfo && setQrCodeInfo(qrCodeInfo);
  };

  useExecuteOnCriteria(complete?.onAction, verificationMutation.isSuccess && generateMfaMutation.isSuccess);

  return (
    <UnauthenticatedContent
      title={t("register.verify.title")}
      infoTitle={t("register.verify.info.title")}
      infoText={t("register.verify.info.text")}
    >
      {showProcessing && <RegisterProcessing>{t("register.processing")}</RegisterProcessing>}
      {showError && <FormErrorBanner>{t("register.verify.error")}</FormErrorBanner>}
      {showInput && (
        <MotionRegisterVerifyForm onSubmit={handleSubmit((data) => onSubmit(data))} layout>
          <Input type={"hidden"} value={email} {...register("email")} />
          <Controller
            control={control}
            name={"code"}
            render={({ field: { value, onChange } }) => <Digits value={value} onChange={onChange} />}
          />
          <Flexbox gap={theme.tyle.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && <Button type={"submit"}>{complete.actionText}</Button>}
          </Flexbox>
        </MotionRegisterVerifyForm>
      )}

      <DevTool control={control} placement={"bottom-right"} />
    </UnauthenticatedContent>
  );
};
