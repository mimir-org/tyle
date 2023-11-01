import { MimirorgQrCodeCm } from "@mimirorg/typelibrary-types";
import { Completion } from "components/Completion/Completion";
import { MultiFactorAuthentication } from "components/MultiFactorAuthentication/MultiFactorAuthentication";
import { RegisterDetails } from "components/Register/RegisterDetails";
import { RegisterVerification } from "components/Register/RegisterVerification";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

export type RegisterSteps = "DETAILS" | "VERIFY" | "MFA" | "COMPLETE";

const Register = () => {
  const { t } = useTranslation("auth");
  const [stage, setStage] = useState<RegisterSteps>("DETAILS");
  const [email, setEmail] = useState("");
  const [mfaInfo, setMfaInfo] = useState<MimirorgQrCodeCm>({ code: "", manualCode: "" });
  const navigate = useNavigate();

  return (
    <>
      {stage === "DETAILS" && (
        <RegisterDetails
          setUserEmail={setEmail}
          complete={{
            actionable: true,
            actionText: t("register.details.submit"),
            onAction: () => setStage("VERIFY"),
          }}
        />
      )}
      {stage === "VERIFY" && (
        <RegisterVerification
          email={email}
          setMfaInfo={setMfaInfo}
          complete={{
            actionable: true,
            actionText: t("register.next"),
            onAction: () => setStage("MFA"),
          }}
          cancel={{
            actionable: true,
            actionText: t("register.back"),
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "MFA" && (
        <MultiFactorAuthentication
          mfaInfo={mfaInfo}
          title={t("register.mfa.title")}
          infoText={t("register.mfa.info.text")}
          codeTitle={t("register.mfa.code.title")}
          manualCodeTitle={t("register.mfa.manual.title")}
          manualCodeDescription={t("register.mfa.manual.description")}
          complete={{
            actionable: true,
            actionText: t("register.next"),
            onAction: () => setStage("COMPLETE"),
          }}
          cancel={{
            actionable: true,
            actionText: t("register.back"),
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "COMPLETE" && (
        <Completion
          title={t("register.complete.title")}
          infoText={t("register.complete.info.text")}
          complete={{
            actionable: true,
            actionText: t("register.complete.return"),
            onAction: () => navigate("/"),
          }}
        />
      )}
    </>
  );
};

export default Register;