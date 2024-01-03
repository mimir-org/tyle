import { CheckCircle, XCircle } from "@styled-icons/heroicons-outline";
import { Theme } from "components/TyleThemeProvider/theme";
import { translucify } from "helpers/theme.helpers";
import { CSSProperties } from "react";
import { DefaultToastOptions } from "react-hot-toast";

export const getCustomToasterStyles = (theme: Theme): DefaultToastOptions => ({
  style: {
    display: "flex",
    padding: `${theme.spacing.base} ${theme.spacing.l}`,
    boxShadow: theme.shadow.small,
    borderRadius: theme.border.radius.medium,
    font: theme.typography.roles.label.large.font,
    letterSpacing: theme.typography.roles.label.large.letterSpacing,
    lineHeight: theme.typography.roles.label.large.lineHeight,
    background: translucify(theme.color.secondary.base, 0.85),
    color: theme.color.secondary.on,
  },
  success: {
    icon: <CheckCircle size={24} style={{ flexShrink: 0 }} />,
    style: {
      background: theme.color.success.base,
      color: theme.color.success.on,
    },
  },
  error: {
    icon: <XCircle size={24} style={{ flexShrink: 0 }} />,
    style: {
      background: theme.color.error.base,
      color: theme.color.error.on,
    },
  },
});

export const getCustomToastBarStyles = (): CSSProperties => ({
  animation: "revert",
});
