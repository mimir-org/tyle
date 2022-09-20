import { CheckCircle, XCircle } from "@styled-icons/heroicons-outline";
import { CSSProperties } from "react";
import { DefaultToastOptions } from "react-hot-toast";
import { TyleTheme } from "../../core";
import { translucify } from "../../mixins";

export const getCustomToasterStyles = (theme: TyleTheme): DefaultToastOptions => ({
  style: {
    display: "flex",
    padding: `${theme.spacing.base} ${theme.spacing.l}`,
    color: theme.color.sys.secondary.on,
    boxShadow: theme.shadow.small,
    borderRadius: theme.border.radius.medium,
    background: translucify(theme.color.sys.secondary.base, 0.85),
    font: theme.typography.sys.roles.label.large.font,
    letterSpacing: theme.typography.sys.roles.label.large.letterSpacing,
    lineHeight: theme.typography.sys.roles.label.large.lineHeight,
  },
  success: {
    icon: <CheckCircle size={24} />,
  },
  error: {
    icon: <XCircle size={24} />,
    style: {
      background: theme.color.sys.error.base,
      color: theme.color.sys.error.on,
    },
  },
});

export const getCustomToastBarStyles = (): CSSProperties => ({
  animation: "revert",
});
