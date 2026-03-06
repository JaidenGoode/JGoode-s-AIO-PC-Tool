import { useEffect, useRef, useCallback } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { detectTweaks } from "@/lib/api";

let globalDetectRunning = false;
let globalDetectScheduled = false;

export function useAutoDetect() {
  const queryClient = useQueryClient();
  const debounceRef = useRef<ReturnType<typeof setTimeout> | null>(null);

  const detectMutation = useMutation({
    mutationFn: detectTweaks,
    onSuccess: () => {
      globalDetectRunning = false;
      if (globalDetectScheduled) {
        globalDetectScheduled = false;
        setTimeout(() => runDetect(), 500);
      }
      queryClient.invalidateQueries({ queryKey: ["/api/tweaks"] });
    },
    onError: () => {
      globalDetectRunning = false;
      globalDetectScheduled = false;
    },
  });

  const runDetect = useCallback(() => {
    if (globalDetectRunning) {
      globalDetectScheduled = true;
      return;
    }
    globalDetectRunning = true;
    detectMutation.mutate();
  }, [detectMutation]);

  const triggerDetect = useCallback((delayMs = 0) => {
    if (debounceRef.current) clearTimeout(debounceRef.current);
    debounceRef.current = setTimeout(runDetect, delayMs);
  }, [runDetect]);

  useEffect(() => {
    return () => {
      if (debounceRef.current) clearTimeout(debounceRef.current);
    };
  }, []);

  return { triggerDetect, isDetecting: detectMutation.isPending };
}
