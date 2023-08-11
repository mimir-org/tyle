import { CheckCircle, XCircle } from "@styled-icons/heroicons-outline";
import { TyleTheme } from "complib/core";
import { translucify } from "complib/mixins";
import { CSSProperties } from "react";
import { DefaultToastOptions } from "react-hot-toast";

export const getCustomToasterStyles = (theme: TyleTheme): DefaultToastOptions => ({
  style: {
    display: "flex",
    padding: `${theme.spacing.base} ${theme.spacing.l}`,
    color: theme.color.secondary.on,
    boxShadow: theme.shadow.small,
    borderRadius: theme.border.radius.medium,
    background: translucify(theme.color.secondary.base, 0.85),
    font: theme.typography.roles.label.large.font,
    letterSpacing: theme.typography.roles.label.large.letterSpacing,
    lineHeight: theme.typography.roles.label.large.lineHeight,
  },
  success: {
    icon: <CheckCircle size={24} style={{ flexShrink: 0 }} />,
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
