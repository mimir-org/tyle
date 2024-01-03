import { toast as reactHotToast } from "react-hot-toast";

// Re-export of react-hot-toast's toast method.
// Should the library need to be replaced this creates a single entry point within the solution itself.
// You could also extend the third party library from here, with your own custom wrapper methods.

/**
 * Call it to create a toast from anywhere, even outside React.
 * Make sure to wrap the application with the component library's theme provider first.
 *
 * @example
 * import { toast } from "...";
 *
 * export const MyComponent () => {
 *   const onClick = toast("You clicked the button");
 *
 *   return <button onClick={() => onClick()}>Action</button>;
 * };
 *
 * @see https://react-hot-toast.com/docs/toast
 */
export const toast = reactHotToast;
