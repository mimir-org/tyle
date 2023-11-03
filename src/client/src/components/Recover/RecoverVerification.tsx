import { DevTool } from "@hookform/devtools";
import { Actionable, Button, Flexbox, Input, Text } from "@mimirorg/component-library";
import { useGenerateMfa } from "api/user.queries";
import AuthContent from "components/AuthContent";
import Digits from "components/Digits";
import Error from "components/Error";
import MotionVerifyForm from "components/MotionVerifyForm";
import Processing from "components/Processing";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { QrCodeView } from "types/authentication/qrCodeView";
import { VerifyRequest } from "types/authentication/verifyRequest";
import { onSubmitForm } from "./RecoverVerification.helpers";

type VerificationProps = Pick<VerifyRequest, "email"> & {
  setMfaInfo: (info: QrCodeView) => void;
  setVerificationInfo?: (info: VerifyRequest) => void;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

const RecoverVerification = ({ email, setMfaInfo, setVerificationInfo, cancel, complete }: VerificationProps) => {
  const theme = useTheme();
  const { t } = useTranslation("auth");
  const { control, register, handleSubmit } = useForm<VerifyRequest>();

  const generateMfaMutation = useGenerateMfa();

  const showProcessing = generateMfaMutation.isLoading;
  const showError = generateMfaMutation.isError;
  const showInput = !showProcessing;

  useExecuteOnCriteria(complete?.onAction, generateMfaMutation.isSuccess);

  return (
    <AuthContent
      title={t("recover.verify.title")}
      firstRow={
        <>
          {showProcessing && <Processing>{t("recover.processing")}</Processing>}
          {showError && <Error>{t("recover.verify.error")}</Error>}
          {showInput && (
            <MotionVerifyForm
              id={"verify-form"}
              onSubmit={handleSubmit((data) =>
                onSubmitForm(data, generateMfaMutation.mutateAsync, setMfaInfo, setVerificationInfo),
              )}
              layout
            >
              <Input type={"hidden"} value={email} {...register("email")} />
              <Controller
                control={control}
                name={"code"}
                render={({ field: { value, onChange } }) => <Digits value={value} onChange={onChange} />}
              />
            </MotionVerifyForm>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("recover.verify.info.text")}</Text>
          <Flexbox gap={theme.mimirorg.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && (
              <Button type={"submit"} form={"verify-form"}>
                {complete.actionText}
              </Button>
            )}
          </Flexbox>
        </>
      }
    />
  );
};

export default RecoverVerification;
