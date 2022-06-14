import { toast as reactHotToast } from "react-hot-toast";

/**
 * Re-exports react hot toast's toast method.
 * Should the library need to be replaced this creates a single entry point within the solution itself.
 * You could also extend the third party library from here, with your own custom methods.
 */
export const toast = reactHotToast;
