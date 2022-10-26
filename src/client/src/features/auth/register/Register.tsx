import { MimirorgQrCodeCm } from "@mimirorg/typelibrary-types";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { Completion } from "../common/Completion";
import { MultiFactorAuthentication } from "../common/MultiFactorAuthentication";
import { RegisterVerification } from "./components/RegisterVerification";
import { RegisterDetails } from "./components/RegisterDetails";

export type RegisterSteps = "DETAILS" | "VERIFY" | "MFA" | "COMPLETE";

export const Register = () => {
  const { t } = useTranslation();
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
            actionText: t("common.next"),
            onAction: () => setStage("MFA"),
          }}
          cancel={{
            actionable: true,
            actionText: t("common.back"),
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
            actionText: t("common.next"),
            onAction: () => setStage("COMPLETE"),
          }}
          cancel={{
            actionable: true,
            actionText: t("common.back"),
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
            actionText: t("common.return"),
            onAction: () => navigate("/"),
          }}
        />
      )}
    </>
  );
};
