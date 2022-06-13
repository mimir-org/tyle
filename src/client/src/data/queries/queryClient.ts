import { QueryClient } from "react-query";

/**
 * A single instance of the QueryClient should be provided to the application's QueryClientProvider wrapper.
 * You can configure this client here, which can then be retrieved through the useQueryClient hook later.
 *
 * @see https://react-query.tanstack.com/reference/QueryClient
 */
export const queryClient = new QueryClient();
